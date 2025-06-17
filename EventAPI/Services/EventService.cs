using EventAPI.Data;
using EventAPI.DTOs;
using EventAPI.Exceptions;
using EventAPI.Models;

namespace EventAPI.Services;

public interface IEventService {
    public Task<Event> CreateEventAsync(PostEventDto postEventDto);
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
}