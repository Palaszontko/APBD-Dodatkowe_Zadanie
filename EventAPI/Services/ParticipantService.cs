using EventAPI.Data;
using EventAPI.Exceptions;
using EventAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Services;

public interface IParticipantService {
    Task<EventParticipant> AddParticipantToEvent(int participantId, int eventId);
    Task CancelParticipantRegister(int participantId, int eventId);

}

public class ParticipantService(AppDbContext data) : IParticipantService {
    public async Task<EventParticipant> AddParticipantToEvent(int participantId, int eventId) {
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

        return eventParticipant;
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
        
        Console.WriteLine(timeLeft);
        Console.WriteLine(now);
        Console.WriteLine((timeLeft - now).TotalHours);

        if ((timeLeft - now).TotalHours >= 24) {
            var affectedRows = await data.EventParticipants.Where(ep => ep.EventId == eventId && ep.ParticipantId == participantId).ExecuteUpdateAsync(
                setters => setters.SetProperty(e => e.Status, "Cancelled"));
            
            await data.SaveChangesAsync();
        }
        else {
            throw new CancelRegisterImpossibleException("Event starts with 24 hours. Cannot cancel participant register. ");
        }


    }
}