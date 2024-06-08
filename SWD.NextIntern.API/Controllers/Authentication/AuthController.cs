using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using SWD.NextIntern.Service.Auth.ForgotPassword;
using SWD.NextIntern.Service.Auth.ResetPassword;
using SWD.NextIntern.Service.Auth.SignIn;
using SWD.NextIntern.Service.Auth.SignUp;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.Services.Auth.RefreshToken;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SWD.NextIntern.API.Controllers.Authentication
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly SignUpCommandHandler _signUpCommandHandler;
        private readonly SignInQueryHandler _signInQueryHandler;
        private readonly ForgotPasswordQueryHandler _forgotPasswordQueryHandler;
        private readonly ResetPasswordCommandHandler _resetPasswordCommandHandler;
        private readonly RefreshTokenCommandHandler _refreshTokenCommandHandler;
        private readonly IJwtService _jwtService;
        private readonly ISender _mediator;
        private readonly IDistributedCache _cache;

        public AuthController(SignUpCommandHandler signUpCommandHandler, SignInQueryHandler signInQueryHandler, ISender mediator, IJwtService _jwtService, IDistributedCache cache, ForgotPasswordQueryHandler forgotPasswordQueryHandler, ResetPasswordCommandHandler resetPasswordCommandHandler, RefreshTokenCommandHandler refreshTokenCommandHandler)
        {
            _signUpCommandHandler = signUpCommandHandler;
            _signInQueryHandler = signInQueryHandler;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _cache = cache;
            _forgotPasswordQueryHandler = forgotPasswordQueryHandler;
            _resetPasswordCommandHandler = resetPasswordCommandHandler;
            _refreshTokenCommandHandler = refreshTokenCommandHandler;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
        {
            try
            {
                var token = await _signUpCommandHandler.Handle(command, default);
                if (token == null)
                    return Unauthorized();
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInQuery query)
        {
            try
            {
                var token = await _signInQueryHandler.Handle(query, default);
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
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordQuery query)
        {
            try
            {  
                if (string.IsNullOrEmpty(query.Email))
                {
                    throw new Exception("Email is required.");
                }
                await _forgotPasswordQueryHandler.Handle(query, default);
                return Ok("OTP and reset link have been sent to your email.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            try
            {
                if (string.IsNullOrEmpty(command.Email) || string.IsNullOrEmpty(command.OTP) || string.IsNullOrEmpty(command.NewPassword))
                {
                    return BadRequest("Email, OTP, and new password are required.");
                }
                await _resetPasswordCommandHandler.Handle(command, default);
                return Ok("Password has been reset successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            try
            {
                if (string.IsNullOrEmpty(command.AccessToken) || string.IsNullOrEmpty(command.RefreshToken))
                {
                    return BadRequest("Invalid access token and refresh token.");
                }
                await _refreshTokenCommandHandler.Handle(command, default);
                return Ok("Refreshed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
