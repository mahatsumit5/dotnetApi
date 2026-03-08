using Microsoft.IdentityModel.Tokens;
using RoyalVilla_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RoyalVilla_API.services
{
    public class JwtService(IConfiguration config)
    {
        private readonly IConfiguration _config = config!;

        public string GenerateJWTToken(User user)
        {

            var key = _config.GetSection("JwtSettings")["Secret"];

            //covert this string key into byte array for cryptography operation
            var byteKey = Encoding.ASCII.GetBytes(key!);

            //This object will define how to create our token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                   [
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name,user.Name.ToString()),
                    new Claim(ClaimTypes.Role,user.Role.ToString())


                   ]),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer= _config.GetSection("JwtSettings")["Issuer"],
                Audience= _config.GetSection("JwtSettings")["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

    }
}
