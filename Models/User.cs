using System.ComponentModel.DataAnnotations;

namespace RoyalVilla_API.Models;

public class User
{
    [Key]
    [Required]

    public  int Id { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Password { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Role { get; set; } = "Customer";

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; } 


}

