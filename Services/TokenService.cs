using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Poc.Authentication.Models;

namespace Poc.Authentication.Services
{
    public class TokenService
    {
        private IConfiguration Configuration { get; }

        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("ApiKey").Value);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var descriptor = new SecurityTokenDescriptor 
            {
                Subject = GenerateClaims(user),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = credentials,
            };

            var token = handler.CreateToken(descriptor);

            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(User user)
        {
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            claims.AddClaim(new Claim("id", user.Id));
            
            foreach (var role in user.Roles)
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}