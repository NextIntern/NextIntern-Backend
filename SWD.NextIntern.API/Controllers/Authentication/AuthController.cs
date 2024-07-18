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
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;

        public AuthController(IMediator mediator, IDistributedCache cache, IJwtService jwtService, ILogger logger, IUserRepository userRepository = null)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _cache = cache;
            _jwtService = jwtService;
            _logger = logger;
            _userRepository = userRepository;
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

        //[HttpGet("signin-google")]
        //public IActionResult SignInGoogle()
        //{
        //    var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleCallback") };
        //    return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        //}

        //[HttpGet("google-callback")]
        //public async Task<IActionResult> GoogleCallback()
        //{
        //    try
        //    {
        //        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        //        if (!result.Succeeded)
        //        {
        //            _logger.LogWarning("Google authentication failed");
        //            return BadRequest("Google authentication failed");
        //        }

        //        var email = result.Principal.FindFirstValue(ClaimTypes.Email);
        //        var fullName = result.Principal.FindFirstValue(ClaimTypes.Name);
        //        var username = email.Split('@')[0]; // Tạo username từ phần đầu của email

        //        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(fullName))
        //        {
        //            _logger.LogWarning("Missing email or fullname from Google authentication");
        //            return BadRequest("Invalid Google account information");
        //        }

        //        var user = await _userRepository.FindAsync(u => u.Email.Equals(email) && u.DeletedDate == null);

        //        if (user == null)
        //        {

        //            user = new User
        //            {
        //                Username = username,
        //                Email = email,
        //                FullName = fullName
        //            };
        //            var createResult = await _mediator.Send(new CreateInternCommand(user), default);
        //            if (!createResult.Succeeded)
        //            {
        //                _logger.LogError("Failed to create new user: {Errors}", string.Join(", ", createResult.Errors));
        //                return StatusCode(500, "Failed to create user account");
        //            }
        //        }
        //        else
        //        {
        //            // Cập nhật thông tin nếu cần
        //            user.FullName = fullName;
        //            await _userService.UpdateUserAsync(user);
        //        }

        //        var token = _jwtService.CreateToken(user);
        //        var refreshToken = _jwtService.GenerateRefreshToken(user);
        //        user.RefreshToken = refreshToken;
        //        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        //        await _userService.UpdateUserAsync(user);

        //        return Ok(new
        //        {
        //            Token = token,
        //            RefreshToken = refreshToken,
        //            User = new { user.Id, user.Username, user.Email, user.FullName }
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error during Google callback");
        //        return StatusCode(500, "An error occurred during authentication");
        //    }
        //}

        //[HttpPost("refresh-token")]
        //public async Task<IActionResult> RefreshToken([FromBody] TokenResponse refreshRequest)
        //{
        //    if (refreshRequest is null)
        //    {
        //        return BadRequest("Invalid client request");
        //    }

        //    string accessToken = refreshRequest.AccessToken;
        //    string refreshToken = refreshRequest.RefreshToken;

        //    var principal = _jwtService.GetPrincipalFromExpiredToken(accessToken);
        //    if (principal == null)
        //    {
        //        return BadRequest("Invalid access token or refresh token");
        //    }

        //    string username = principal.Identity.Name;

        //    var user = await _userService.GetUserByUsernameAsync(username);

        //    if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        //    {
        //        return BadRequest("Invalid access token or refresh token");
        //    }

        //    var newAccessToken = _jwtService.CreateToken(user);
        //    var newRefreshToken = _jwtService.GenerateRefreshToken(user);

        //    user.RefreshToken = newRefreshToken;
        //    await _userService.UpdateUserAsync(user);

        //    return Ok(new
        //    {
        //        AccessToken = newAccessToken,
        //        RefreshToken = newRefreshToken
        //    });
        //}

        //[HttpPost("signout")]
        //[Authorize]
        //public async Task<IActionResult> SignOut()
        //{
        //    var username = User.Identity.Name;
        //    var user = await _userService.GetUserByUsernameAsync(username);
        //    if (user != null)
        //    {
        //        user.RefreshToken = null;
        //        await _userService.UpdateUserAsync(user);
        //    }

        //    await HttpContext.SignOutAsync();
        //    return Ok(new { message = "Signed out successfully" });
        //}
    }
}
