using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Repository.IRepositories;
using SWD.NextIntern.Service.Auth.ForgotPassword;
using SWD.NextIntern.Service.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.Auth.RefreshToken
{
    public class RefreshTokenCommandHandler
    {
        private readonly IJwtService _jwtService;
        private readonly IInternRepository _internRepository;

        public RefreshTokenCommandHandler(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public async Task<IActionResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = _jwtService.GetPrincipalFromExpiredToken(request.AccessToken);
            var username = principal.Identity.Name;
            var user = await _internRepository.FindAsync(u => u.Username.Equals(username));

            var expirationDate = _jwtService.GetPrincipalFromExpiredToken(request.AccessToken);

            //if (DateTime.UtcNow < expirationDate)
            //    return BadRequest("Token has not expired yet");

            var newAccessToken = _jwtService.CreateToken(user.Id.ToString(), "User");
            var newRefreshToken = _jwtService.GenerateRefreshToken(user.Id.ToString(), "User");

            return new ObjectResult(new
            {
                token = newAccessToken,
                refreshToken = newRefreshToken
            });
        }
    }
}
