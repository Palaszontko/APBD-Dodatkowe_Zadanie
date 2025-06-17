using System.ComponentModel.DataAnnotations;

namespace EventAPI.Models;

public class Event {
    [Key] public int Id { get; set; }

    [MaxLength(50)] public string Title { get; set; } = null!;

    [MaxLength(500)] public string? Description { get; set; }

    public DateTime Date { get; set; }

    public int MaxPeople { get; set; }

    public virtual ICollection<EventSpeaker> EventSpeakers { get; set; } = new List<EventSpeaker>();
}