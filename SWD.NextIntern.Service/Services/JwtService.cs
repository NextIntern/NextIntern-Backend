
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using SWD.NextIntern.Service.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text;
using SWD.NextIntern.Repository.Entities;
using System.Net;
using SWD.NextIntern.Repository.IRepositories;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Repositories.IRepositories;

namespace SWD.NextIntern.Service
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly string _issuer;
        private readonly IUserRepository _userRepository;
        private readonly IDistributedCache _cache;

        public JwtService(IConfiguration configuration, IUserRepository userRepository, IDistributedCache cache)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _cache = cache;
        }


        public async Task<string> CreateToken(string ID, string roles)
        {
            //var key = Encoding.UTF8.GetBytes("swdnextinterniumaycauratnhiu!@#$");
            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(key);
            //}

            var authority = _configuration["Security:Bearer:Authority"];
            var audience = _configuration["Security:Bearer:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
            var securityKey = new SymmetricSecurityKey(key);
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var existingUser = await _userRepository.FindAsync(i => i.UserId.ToString().Equals(ID));
            var claims = new List<Claim>
                {
                   new Claim(JwtRegisteredClaimNames.Sub, ID),
                   new Claim(JwtRegisteredClaimNames.Email, existingUser.Email),
                   new Claim("name", existingUser.Username),
                   //new Claim("admin", "false"),
                   new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                   new Claim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(15).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                   new Claim(JwtRegisteredClaimNames.Aud, audience),
                   new Claim("roles", roles),
                 };

            var identity = new ClaimsIdentity(claims);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            await _cache.SetStringAsync("Token_" + ID, tokenString, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });

            return tokenString;
        }


        public async Task<string> GenerateRefreshToken(string ID, string roles)
        {
            var authority = _configuration["Security:Bearer:Authority"];
            var audience = _configuration["Security:Bearer:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
            var securityKey = new SymmetricSecurityKey(key);
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var existingIntern = await _userRepository.FindAsync(i => i.UserId.ToString().Equals(ID));
            var claims = new List<Claim>
                {
                  new Claim(JwtRegisteredClaimNames.Sub, ID),
                   new Claim(JwtRegisteredClaimNames.Email, existingIntern.Email),
                   new Claim("name", existingIntern.Username),
                   //new Claim("admin", "false"),
                   new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                   new Claim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(15).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                   new Claim(JwtRegisteredClaimNames.Aud, audience),
                   new Claim("roles", roles),
                 };

            var identity = new ClaimsIdentity(claims);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            await _cache.SetStringAsync("RefreshToken_" + ID, tokenString, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
            });

            return tokenString;
        }


        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            //var key = new byte[32];
            //using (var rng = randomnumbergenerator.create())
            //{
            //    rng.getbytes(key);
            //}
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
            var securityKey = new SymmetricSecurityKey(key);
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenValidationParameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token.");

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

            throw new Exception("Token does not contain expiration claim.");
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");
                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

