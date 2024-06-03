using MediatR;
using Microsoft.AspNetCore.Mvc;
using NextIntern.Application.Auth.SignIn;
using NextIntern.Application.Auth.SignUp;
using NextIntern.Application.Common.Interfaces;
using NextIntern.Application.InternQuery;
using System.IdentityModel.Tokens.Jwt;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NextIntern.API.Controllers.Authentication
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly SignUpCommandHandler _signUpCommandHandler;
        private readonly SignInQueryHandler _signInQueryHandler;
        private readonly IJwtService _jwtService;
        private readonly ISender _mediator;

        public AuthController(SignUpCommandHandler signUpCommandHandler, SignInQueryHandler signInQueryHandler, ISender mediator, IJwtService _jwtService)
        {
            _signUpCommandHandler = signUpCommandHandler;
            _signInQueryHandler = signInQueryHandler;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
    }
}
