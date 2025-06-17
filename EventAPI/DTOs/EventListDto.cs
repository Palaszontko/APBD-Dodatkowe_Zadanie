using System.ComponentModel.DataAnnotations;
using EventAPI.Models;

namespace EventAPI.DTOs;

public class EventListDto {
    public int Id { get; set; }
    
    public string Title { get; set; } = null!;
    
    public string? Description { get; set; }
    
    public DateTime Date { get; set; }
    public int MaxPeople { get; set; }

    public List<SpeakerDto> Speakers { get; set; } = new List<SpeakerDto>();
    public int RegisteredParticipantsCount { get; set; }
    
    public int AvailableSpots { get; set; }
}
