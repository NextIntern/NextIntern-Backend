//using MediatR;
//using Microsoft.AspNetCore.Http;
//using SWD.NextIntern.Repository.Repositories.IRepositories;
//using SWD.NextIntern.Service.DTOs.Responses;
//using System.IdentityModel.Tokens.Jwt;

//namespace SWD.NextIntern.Service.Services.RoleService.GetAll
//{
//    public class CheckRoleQueryHandler : IRequestHandler<CheckRoleQuery, ResponseObject<string>>
//    {
//        private readonly IRoleRepository _roleRepository;

//        public CheckRoleQueryHandler(IRoleRepository roleRepository)
//        {
//            _roleRepository = roleRepository;
//        }

//        public async Task<ResponseObject<string>> Handle(CheckRoleQuery request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                // Lấy token từ request
//                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

//                if (token == null)
//                {
//                    return Unauthorized("Token is missing");
//                }

//                // Giải mã token
//                var handler = new JwtSecurityTokenHandler();
//                var jwtToken = handler.ReadJwtToken(token);

//                // Lấy các claims từ token
//                var roleClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "role")?.Value;

//                if (roleClaim == null)
//                {
//                    return Forbid("Role claim is missing in the token");
//                }

//                // Kiểm tra role
//                if (roleClaim == "Admin")
//                {
//                    return Ok("User is an Admin");
//                }
//                else
//                {
//                    return Ok("User is not an Admin");
//                }
//            }
//            catch (Exception ex)
//            {
//                return BadRequest($"Error: {ex.Message}");
//            }
//        }
//    }
//}
