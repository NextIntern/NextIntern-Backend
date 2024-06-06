using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NextIntern.Application.Auth.ForgotPassword;
using NextIntern.Application.Auth.SignIn;
using NextIntern.Application.Auth.SignUp;
using NextIntern.Application.Common.Interfaces;

namespace NextIntern.API.Controllers.Authentication
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly SignUpCommandHandler _signUpCommandHandler;
        private readonly SignInQueryHandler _signInQueryHandler;
        private readonly ForgotPasswordCommandHandler _forgotPasswordCommandHandler;
        private readonly IJwtService _jwtService;
        private readonly ISender _mediator;
        private readonly IDistributedCache _cache;

        public AuthController(SignUpCommandHandler signUpCommandHandler, SignInQueryHandler signInQueryHandler, ISender mediator, IJwtService _jwtService, IDistributedCache cache, ForgotPasswordCommandHandler forgotPasswordCommandHandler)
        {
            _signUpCommandHandler = signUpCommandHandler;
            _signInQueryHandler = signInQueryHandler;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _cache = cache;
            _forgotPasswordCommandHandler = forgotPasswordCommandHandler;
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
                await _forgotPasswordCommandHandler.Handle(query, default);
                return Ok("OTP and reset link have been sent to your email.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
