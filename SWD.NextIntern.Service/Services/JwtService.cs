
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using SWD.NextIntern.Service.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace SWD.NextIntern.Service
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string CreateToken(string ID, string roles)
        {
            var claims = new List<Claim> {

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
                expires: DateTime.Now.AddHours(1),

                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public string GenerateRefreshToken(string ID, string roles)
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

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }

            var securityKey = new SymmetricSecurityKey(key);
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false, // Ignore expiration date
                ValidateIssuerSigningKey = true,
                //ValidIssuer = _configuration["Jwt:Issuer"],
                //ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = securityKey
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public DateTime GetExpirationDateFromToken(string token)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            var expirationClaim = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);

            if (expirationClaim != null)
            {
                var exp = long.Parse(expirationClaim.Value);
                var expirationTime = DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;
                return expirationTime;
            }

            throw new Exception("Token does not contain expiration claim");
        }
    }
}

