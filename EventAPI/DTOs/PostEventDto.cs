using System.ComponentModel.DataAnnotations;

namespace EventAPI.DTOs;

public class PostEventDto {
    [Required]
    [MaxLength(50)]
    public string Title { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "MaxPeople must be greater than 0")]
    public int MaxPeople { get; set; }
}