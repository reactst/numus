using Microsoft.IdentityModel.Tokens;
using Numus.Shared.Interfaces;
using Numus.Shared.Dtoes.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Numus.Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool CheckPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        }

        public string GenerateJwtToken(UserDto userDTO)
        {
            // Create claims for the user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userDTO.ExternalId.ToString()),
                new Claim(ClaimTypes.Name, userDTO.Name),
                new Claim(ClaimTypes.Role, userDTO.Role.ToString())
            };

            // Get JWT secret from configuration
            var jwtSecret = _configuration["JWTSecret"];

            // Generate token
            var key = Encoding.ASCII.GetBytes(jwtSecret);
            var tokenDescriptor = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return token;
        }
    }
}
