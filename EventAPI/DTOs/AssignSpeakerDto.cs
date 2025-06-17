using System.ComponentModel.DataAnnotations;

namespace EventAPI.DTOs;

public class AssignSpeakerDto {
    [Required]
    public int EventId { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "At least one speaker must be provided")]
    public List<int> SpeakerIds { get; set; } = new();
} 