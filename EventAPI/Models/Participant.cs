using System.ComponentModel.DataAnnotations;

namespace EventAPI.Models;

public class Participant {
    [Key] public int Id { get; set; }

    [MaxLength(50)] public string FirstName { get; set; } = null!;

    [MaxLength(50)] public string LastName { get; set; } = null!;

    [MaxLength(100)] public string? Email { get; set; }
}