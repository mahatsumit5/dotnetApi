using System.ComponentModel.DataAnnotations;

namespace RoyalVilla_API.dtos
{
    public class UserDTO
    {
        public  int Id { get; set; }

        [Required]
        public string Email { get; set; } = default!;

        [Required]
        public  string Name { get; set; }= default!;

        public  string Role { get; set; } = default!;

    }

    public class LoginRequestDTO
    {

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }

    public class LoginResponseDTO
    {
        public string? Token { get; set; }

        public UserDTO? UserDto { get; set; }

    }

    public class RegisterRequestDTO
    {

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Password { get; set; }


        [MaxLength(50)]
        public string Role { get; set; } = "Customer";
    }
}
