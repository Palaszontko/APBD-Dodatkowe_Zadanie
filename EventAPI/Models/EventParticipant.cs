using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Models;

[Table("EventParticipant")]
[PrimaryKey(nameof(EventId), nameof(ParticipantId))]
public class EventParticipant {
    [Column("Event_Id")] public int EventId { get; set; }

    [Column("Participant_Id")] public int ParticipantId { get; set; }

    public DateTime RegisterDate { get; set; }

    [MaxLength(50)] public string Status { get; set; } = null!;

    public DateTime? CancelDate { get; set; }

    [ForeignKey(nameof(EventId))] public virtual Event Event { get; set; } = null!;

    [ForeignKey(nameof(ParticipantId))] public virtual Participant Participant { get; set; } = null!;
}