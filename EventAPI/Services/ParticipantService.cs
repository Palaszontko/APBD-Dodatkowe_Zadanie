using EventAPI.Data;
using EventAPI.Exceptions;
using EventAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Services;

public interface IParticipantService {
    Task<EventParticipant> AddParticipantToEvent(int participantId, int eventId);
}

public class ParticipantService(AppDbContext context) : IParticipantService {
    public async Task<EventParticipant> AddParticipantToEvent(int participantId, int eventId) {
        var currentEvent = await context.Events.FindAsync(eventId);
        if (currentEvent == null) {
            throw new NotFoundException($"Event with ID {eventId} not found.");
        }
        
        var participant = await context.Participants.FindAsync(participantId);
        if (participant == null) {
            throw new NotFoundException($"Participant with ID {participantId} not found.");
        }
        
        var existingRegistration = await context.EventParticipants
            .FirstOrDefaultAsync(ep => ep.EventId == eventId && ep.ParticipantId == participantId);
        
        if (existingRegistration != null) {
            throw new ParticipantAlreadyRegisteredException(
                $"Participant {participantId} is already registered for event {eventId}.");
        }
        
        var currentParticipants = await context.EventParticipants
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

        await context.EventParticipants.AddAsync(eventParticipant);
        await context.SaveChangesAsync();

        return eventParticipant;
    }
   
}