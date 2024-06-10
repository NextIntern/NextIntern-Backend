using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWD.NextIntern.Repository.IRepositories;
using SWD.NextIntern.Service.Auth.ForgotPassword;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.Auth.RefreshToken
{
    public class RefreshTokenCommandHandler
    {
        private readonly IJwtService _jwtService;
        private readonly IInternRepository _internRepository;
        private readonly IDistributedCache _cache;

        public RefreshTokenCommandHandler(IJwtService jwtService, IInternRepository internRepository, IDistributedCache cache)
        {
            _jwtService = jwtService;
            _internRepository = internRepository;
            _cache = cache;
        }

        public async Task<TokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            string[] jwtParts = request.AccessToken.Split('.');
            string decodedPayload = DecodeBase64Url(jwtParts[1]);
            JsonDocument payloadJson = JsonDocument.Parse(decodedPayload);
            string username = payloadJson.RootElement.GetProperty("name").GetString();

            var user = await _internRepository.FindAsync(u => u.Username.Equals(username));
            _cache.RemoveAsync("Token_" + user.UserId.ToString());
            _cache.RemoveAsync("RefreshToken_" + user.UserId.ToString());
            var newAccessToken = await _jwtService.CreateToken(user.UserId.ToString(), "User");
            var newRefreshToken = await _jwtService.GenerateRefreshToken(user.UserId.ToString(), "User");

            if (newAccessToken != null || newRefreshToken != null)
            {
                return null;
            }

            await _cache.SetStringAsync("Token_" + user.Id, newAccessToken.ToString(), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });

            await _cache.SetStringAsync("RefreshToken_" + user.Id, newRefreshToken.ToString(), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
            });

            return new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public static string DecodeBase64Url(string base64Url)
        {
            string base64 = base64Url.Replace('-', '+').Replace('_', '/');
                    string padding = new string('=', (4 - base64.Length % 4) % 4);
                    string base64String = base64 + padding;
                    byte[] data = Convert.FromBase64String(base64String);
                    return Encoding.UTF8.GetString(data);
        }
    }
}
