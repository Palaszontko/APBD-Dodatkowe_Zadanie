using EventAPI.Data;
using EventAPI.DTOs;
using EventAPI.Exceptions;
using EventAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Services;

public interface ISpeakerService {
    Task<EventWithSpeakersDto> AddSpeakersToEventAsync(AssignSpeakerDto assignSpeakerDto);
}

public class SpeakerService(AppDbContext data) : ISpeakerService {
    public async Task<EventWithSpeakersDto> AddSpeakersToEventAsync(AssignSpeakerDto assignSpeakerDto) {
        var currentEvent = await data.Events.FindAsync(assignSpeakerDto.EventId);
        if (currentEvent == null) {
            throw new NotFoundException($"Event with ID {assignSpeakerDto.EventId} not found.");
        }
        
        var speakers = await data.Speakers
            .Where(s => assignSpeakerDto.SpeakerIds.Contains(s.Id))
            .ToListAsync();

        if (speakers.Count != assignSpeakerDto.SpeakerIds.Count) {
            var foundIds = speakers.Select(s => s.Id);
            var notFoundIds = assignSpeakerDto.SpeakerIds.Except(foundIds);
            throw new NotFoundException($"Speakers with IDs {string.Join(", ", notFoundIds)} not found.");
        }
        
        var existingAssignments = await data.EventSpeakers
            .Where(es => es.EventId == assignSpeakerDto.EventId && assignSpeakerDto.SpeakerIds.Contains(es.SpeakerId))
            .ToListAsync();

        if (existingAssignments.Any()) {
            var existingIds = existingAssignments.Select(es => es.SpeakerId);
            throw new SpeakerConflictException(
                $"Speakers with IDs {string.Join(", ", existingIds)} are already assigned to this event.");
        }
            
        var conflictingEvents = await data.EventSpeakers
            .Include(es => es.Event)
            .Where(es => assignSpeakerDto.SpeakerIds.Contains(es.SpeakerId) && es.Event.Date == currentEvent.Date)
            .ToListAsync();

        if (conflictingEvents.Any()) {
            var conflictingSpeakers = conflictingEvents
                .GroupBy(es => es.SpeakerId)
                .Select(g => new {
                    SpeakerId = g.Key,
                    Events = g.Select(es => es.EventId)
                });

            var conflictMessages = conflictingSpeakers
                .Select(cs => $"Speaker {cs.SpeakerId} is already assigned to events {string.Join(", ", cs.Events)}");

            throw new SpeakerConflictException(
                $"Scheduling conflicts found:\n{string.Join("\n", conflictMessages)}");
        }
        
        var eventSpeakers = assignSpeakerDto.SpeakerIds.Select(speakerId => new EventSpeaker {
            EventId = assignSpeakerDto.EventId,
            SpeakerId = speakerId
        }).ToList();

        await data.EventSpeakers.AddRangeAsync(eventSpeakers);
        await data.SaveChangesAsync();
        
        var updatedEvent = await data.Events
            .Include(e => e.EventSpeakers)
            .ThenInclude(es => es.Speaker)
            .FirstOrDefaultAsync<Event>(e => e.Id == assignSpeakerDto.EventId);

        if (updatedEvent == null) {
            throw new NotFoundException("Failed to get updated event data.");
        }

        return new EventWithSpeakersDto {
            Id = updatedEvent.Id,
            Title = updatedEvent.Title,
            Description = updatedEvent.Description,
            Date = updatedEvent.Date,
            MaxPeople = updatedEvent.MaxPeople,
            Speakers = updatedEvent.EventSpeakers
                .Select(es => new SpeakerDto {
                    Id = es.Speaker.Id,
                    FirstName = es.Speaker.FirstName,
                    LastName = es.Speaker.LastName
                })
                .ToList()
        };
    }
}