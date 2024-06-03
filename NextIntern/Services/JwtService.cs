using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using NextIntern.Application.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace NextIntern.Services
{
    public class JwtService : IJwtService
    {
        public string CreateToken(string ID, string roles)
        {
            var claims = new List<Claim>
            {

                new(JwtRegisteredClaimNames.Sub, ID),
                new(ClaimTypes.Role, roles)
            };

            var key = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }

            var securityKey = new SymmetricSecurityKey(key);
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                // issuer: "test",
                // audience: "api",
                claims: claims,
                expires: DateTime.Now.AddYears(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}