using EventAPI.Models;

namespace EventAPI.DTOs;

public class EventWithSpeakersDto {
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public int MaxPeople { get; set; }
    public List<SpeakerDto> Speakers { get; set; } = new();
}

public class SpeakerDto {
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
} 