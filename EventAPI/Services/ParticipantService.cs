using EventAPI.Data;
using EventAPI.DTOs;
using EventAPI.Exceptions;
using EventAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Services;

public interface IParticipantService {
    Task<ParticipantReportDto> AddParticipantToEvent(int participantId, int eventId);
    Task CancelParticipantRegister(int participantId, int eventId);
    Task<ICollection<ParticipantReportDto>> GetReportForParticipants();
}

public class ParticipantService(AppDbContext data) : IParticipantService {
    public async Task<ParticipantReportDto> AddParticipantToEvent(int participantId, int eventId) {
        var currentEvent = await data.Events.FindAsync(eventId);
        if (currentEvent == null) {
            throw new NotFoundException($"Event with ID {eventId} not found.");
        }
        
        var participant = await data.Participants.FindAsync(participantId);
        if (participant == null) {
            throw new NotFoundException($"Participant with ID {participantId} not found.");
        }
        
        var existingRegistration = await data.EventParticipants
            .FirstOrDefaultAsync(ep => ep.EventId == eventId && ep.ParticipantId == participantId);
        
        if (existingRegistration != null) {
            throw new ParticipantAlreadyRegisteredException(
                $"Participant {participantId} is already registered for event {eventId}.");
        }
        
        var currentParticipants = await data.EventParticipants
            .CountAsync(ep => ep.EventId == eventId && ep.Status == "Registered");
        
        if (currentParticipants >= currentEvent.MaxPeople) {
            throw new TooManyPeopleException(
                $"Event {eventId} has reached its maximum capacity of {currentEvent.MaxPeople} participants.");
        }
        
        var eventParticipant = new EventParticipant {
            EventId = eventId,
            ParticipantId = participantId,
            RegisterDate = DateTime.Now,
            Status = "Registered"
        };

        await data.EventParticipants.AddAsync(eventParticipant);
        await data.SaveChangesAsync();

        return await data.Participants
            .Include(p => p.EventParticipants)
                .ThenInclude(ep => ep.Event)
                    .ThenInclude(e => e.EventSpeakers)
                        .ThenInclude(es => es.Speaker)
            .Where(p => p.Id == participantId)
            .Select(p => new ParticipantReportDto {
                ParticipantId = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Events = p.EventParticipants
                    .Select(ep => new EventReportDto {
                        EventId = ep.EventId,
                        Title = ep.Event.Title,
                        Date = ep.Event.Date,
                        RegisterDate = ep.RegisterDate,
                        Status = ep.Status,
                        Speakers = ep.Event.EventSpeakers
                            .Select(es => new SpeakerDto {
                                Id = es.Speaker.Id,
                                FirstName = es.Speaker.FirstName,
                                LastName = es.Speaker.LastName
                            })
                            .ToList()
                    })
                    .ToList()
            })
            .FirstAsync();
    }
   
    public async Task CancelParticipantRegister(int participantId, int eventId) {
        var currentEvent = await data.Events.FindAsync(eventId);
        if (currentEvent == null) {
            throw new NotFoundException($"Event with ID {eventId} not found.");
        }
        
        var participant = await data.Participants.FindAsync(participantId);
        if (participant == null) {
            throw new NotFoundException($"Participant with ID {participantId} not found.");
        }
        
        var participantEvent = await data.EventParticipants.FindAsync(eventId, participantId);

        if (participantEvent == null) {
            throw new NotFoundException($"Register with ID participant {participantId} and ID event {eventId} not found.");
        }

        var timeLeft = currentEvent.Date;
        var now = DateTime.Now;

        if ((timeLeft - now).TotalHours >= 24) {
            var affectedRows = await data.EventParticipants.Where(ep => ep.EventId == eventId && ep.ParticipantId == participantId).ExecuteUpdateAsync(
                setters => setters.SetProperty(e => e.Status, "Cancelled"));
            
            await data.SaveChangesAsync();
        }
        else {
            throw new CancelRegisterImpossibleException("Event starts with 24 hours. Cannot cancel participant register. ");
        }


    }
    public async Task<ICollection<ParticipantReportDto>> GetReportForParticipants() {
        return await data.Participants
            .Include(p => p.EventParticipants)
                .ThenInclude(ep => ep.Event)
                    .ThenInclude(e => e.EventSpeakers)
                        .ThenInclude(es => es.Speaker)
            .Select(p => new ParticipantReportDto {
                ParticipantId = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Events = p.EventParticipants
                    .Where(ep => ep.Status == "Registered")
                    .Select(ep => new EventReportDto {
                        EventId = ep.EventId,
                        Title = ep.Event.Title,
                        Date = ep.Event.Date,
                        RegisterDate = ep.RegisterDate,
                        Status = ep.Status,
                        Speakers = ep.Event.EventSpeakers
                            .Select(es => new SpeakerDto {
                                Id = es.Speaker.Id,
                                FirstName = es.Speaker.FirstName,
                                LastName = es.Speaker.LastName
                            })
                            .ToList()
                    })
                    .ToList()
            })
            .ToListAsync();
    }
}