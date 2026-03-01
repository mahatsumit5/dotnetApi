using RoyalVilla_API.dtos;

namespace RoyalVilla_API.services
{
    public interface IAuthService
    {
        Task<UserDTO?> RegisterAsync(RegisterRequestDTO registerReqDto);

        Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO loginReqDto);

        Task<bool> DoesEmailExistsAsync(string email);
    }
}
