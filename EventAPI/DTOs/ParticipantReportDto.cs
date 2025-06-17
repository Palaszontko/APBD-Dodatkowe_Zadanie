using EventAPI.Models;

namespace EventAPI.DTOs;

public class ParticipantReportDto {
    public int ParticipantId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public List<EventReportDto> Events { get; set; } = new();
}

public class EventReportDto {
    public int EventId { get; set; }
    public string Title { get; set; } = null!;
    public DateTime Date { get; set; }
    public DateTime RegisterDate { get; set; }
    public string Status { get; set; } = null!;
    public List<SpeakerDto> Speakers { get; set; } = new();
}