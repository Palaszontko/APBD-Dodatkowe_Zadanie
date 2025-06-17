using EventAPI.Data;
using EventAPI.DTOs;
using EventAPI.Exceptions;
using EventAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Services;

public interface IEventService {
    public Task<Event> CreateEventAsync(PostEventDto postEventDto);
    public Task<ICollection<EventListDto>> GetAllEvents();
}

public class EventService(AppDbContext data) : IEventService {
    public async Task<Event> CreateEventAsync(PostEventDto postEventDto) {

        if (postEventDto.Date < DateTime.Now) {
            throw new EventInPastException("Event date cannot be earlier than now.");
        }

        var newEvent = new Event {
            Title = postEventDto.Title,
            Description = postEventDto.Description,
            Date = postEventDto.Date,
            MaxPeople = postEventDto.MaxPeople
        };

        await data.Events.AddAsync(newEvent);
        await data.SaveChangesAsync();

        return new Event {
            Id = newEvent.Id,
            Title = newEvent.Title,
            Description = newEvent.Description,
            Date = newEvent.Date,
            MaxPeople = newEvent.MaxPeople
        };
    }

    public async Task<ICollection<EventListDto>> GetAllEvents() {
        return await data.Events
            .Select(e => new EventListDto {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Date = e.Date,
                MaxPeople = e.MaxPeople,
                Speakers = e.EventSpeakers
                    .Select(es => new SpeakerDto {
                        Id = es.Speaker.Id,
                        FirstName = es.Speaker.FirstName,
                        LastName = es.Speaker.LastName
                    })
                    .ToList(),
                RegisteredParticipantsCount = data.EventParticipants
                    .Count(ep => ep.EventId == e.Id && ep.Status == "Registered"),
                AvailableSpots = e.MaxPeople - data.EventParticipants
                    .Count(ep => ep.EventId == e.Id && ep.Status == "Registered")
            })
            .ToListAsync();
    }
}