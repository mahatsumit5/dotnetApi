using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoyalVilla_API.Database;
using RoyalVilla_API.dtos;
using RoyalVilla_API.Models;

namespace RoyalVilla_API.services
{
    public class AuthService(ApplicationDbContext db, IMapper mapper) : IAuthService
    {
        private readonly ApplicationDbContext _db = db;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> DoesEmailExistsAsync(string email)
        {
            return await _db.Users.AnyAsync(u => u.Email.ToLower()==email.ToLower());
        }

        public Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO loginReqDto)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO?> RegisterAsync(RegisterRequestDTO registerReqDto)
        {
            if (await DoesEmailExistsAsync(registerReqDto.Email))
            {
                return null;
            }
            User user = new()
            {
                Email = registerReqDto.Email,
                Name = registerReqDto.Name,
                Password = registerReqDto.Password,
                Role=string.IsNullOrEmpty(registerReqDto.Role)?"Customer":registerReqDto.Role,
                CreatedAt = DateTime.Now,
            };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return _mapper.Map<UserDTO>(user);

        }
    }
}
