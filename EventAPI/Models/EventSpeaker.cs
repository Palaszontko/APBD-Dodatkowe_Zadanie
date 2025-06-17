using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Models;

[Table("EventSpeaker")]
[PrimaryKey(nameof(EventId), nameof(SpeakerId))]
public class EventSpeaker {
    [Column("Event_Id")] public int EventId { get; set; }

    [Column("Speaker_Id")] public int SpeakerId { get; set; }

    [ForeignKey(nameof(EventId))] public virtual Event Event { get; set; } = null!;

    [ForeignKey(nameof(SpeakerId))] public virtual Speaker Speaker { get; set; } = null!;
}