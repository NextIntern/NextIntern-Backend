using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextIntern.Application.DTOs;

namespace NextIntern.API.Controllers.Authentication
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.LoginAsync(request);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { Token = token });
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequest request)
        {
            var result = await _authService.SignupAsync(request);
            if (!result)
            {
                return BadRequest("User already exists.");
            }

            return Ok();
        }
    }
}
