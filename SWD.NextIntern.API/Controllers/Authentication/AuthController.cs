using MediatR;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using SWD.NextIntern.Service.Auth.ForgotPassword;
using SWD.NextIntern.Service.Auth.ResetPassword;
using SWD.NextIntern.Service.Auth.SignIn;
using SWD.NextIntern.Service.Auth.SignUp;
using SWD.NextIntern.Service.Services.Auth.RefreshToken;
using SWD.NextIntern.Service;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Repository.Entities;
using System.Security.Claims;
using IdentityModel.Client;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.InternService.Create;

namespace SWD.NextIntern.API.Controllers.Authentication
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _cache;
        private readonly IJwtService _jwtService;
        //private readonly ILogger _logger;
        //private readonly IUserRepository _userRepository;

        public AuthController(IMediator mediator, IDistributedCache cache, IJwtService jwtService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _cache = cache;
            _jwtService = jwtService;
        }

        //[HttpPost("signup")]
        //[AllowAnonymous]
        //public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
        //{
        //    try
        //    {
        //        var token = await _mediator.Send(command, default);
        //        if (token == null)
        //            return Unauthorized();
        //        return Ok(new { token });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] SignInQuery query)
        {
            try
            {
                var token = await _mediator.Send(query, default);
                if (token == null)
                    return Unauthorized();
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordQuery query)
        {
            try
            {
                if (string.IsNullOrEmpty(query.Email))
                {
                    throw new Exception("Email is required.");
                }
                await _mediator.Send(query, default);
                return Ok("OTP and reset link have been sent to your email.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("resetpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            try
            {
                if (string.IsNullOrEmpty(command.Email) || string.IsNullOrEmpty(command.OTP) || string.IsNullOrEmpty(command.NewPassword))
                {
                    return BadRequest("Email, OTP, and new password are required.");
                }
                await _mediator.Send(command, default);
                return Ok("Password has been reset successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //[HttpPost("refreshtoken")]
        //[Authorize]
        //public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(command.AccessToken) || string.IsNullOrEmpty(command.RefreshToken))
        //        {
        //            return BadRequest("Invalid access token and refresh token.");
        //        }
        //        var token = await _mediator.Send(command, default);
        //        if (token == null)
        //            return Unauthorized();
        //        return Ok(new { token });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        [HttpGet("signin-google")]
        public IActionResult SignInGoogle()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleCallback") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-callback")]
        public async Task<IActionResult> GoogleCallback()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                return BadRequest("Google authentication failed");
            }

            var token = _jwtService.GenerateJwtTokenGoogle(result.Principal);
            var tokenString = token.Result;
            //return Ok(tokenString);

            return Redirect($"https://nextintern.tech/dashboard?token=tokenString");
        }

        [HttpPost("signout")]
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return Ok(new { message = "Signed out" });
        }
    }
}
