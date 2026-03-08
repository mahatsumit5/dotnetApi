using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RoyalVilla_API.Database;
using RoyalVilla_API.dtos;
using RoyalVilla_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RoyalVilla_API.services
{
    public class AuthService(ApplicationDbContext db, IMapper mapper, IConfiguration config) : IAuthService
    {
        private readonly ApplicationDbContext _db = db;
        private readonly IMapper _mapper = mapper;
        private readonly IConfiguration _config = config;
        public async Task<bool> DoesEmailExistsAsync(string email)
        {
            return await _db.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO loginReqDto)
        {
            if (loginReqDto.Email == null)
            {
                return null;
            }
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == loginReqDto.Email.ToLower());
            Console.WriteLine(user);

            if (user == null)
            {
                throw new UnauthorizedAccessException("No user found");
            }
            var jwtService = new JwtService(_config);
            var token = jwtService.GenerateJWTToken(user);
            return new LoginResponseDTO()
            {
                User = _mapper.Map<UserDTO>(user),
                Token = token
            };
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
                Role = string.IsNullOrEmpty(registerReqDto.Role) ? "Customer" : registerReqDto.Role,
                CreatedAt = DateTime.Now,
            };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return _mapper.Map<UserDTO>(user);

        }

       
    }



}
