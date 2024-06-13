using MediatR;
using SWD.NextIntern.Repository.IRepositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Auth.SignIn
{
    public class SignInQueryHandler : IRequestHandler<SignInQuery, TokenResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IRoleRepository _roleRepository;

        public SignInQueryHandler(IUserRepository userRepository, IJwtService jwtService, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _roleRepository = roleRepository;
        }

        public async Task<TokenResponse> Handle(SignInQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.FindAsync(i => request.Username.Equals(i.Username));
            var role = await _roleRepository.FindAsync(r => existingUser.RoleId == r.RoleId);
            string roleName = role.RoleName;

            if (existingUser == null || !BCrypt.Net.BCrypt.Verify(request.Password, existingUser.Password))
            {
                throw new Exception("Invalid username or password.");
            }

            return new TokenResponse
            {
                AccessToken = await _jwtService.CreateToken(existingUser.UserId.ToString(), roleName),
                RefreshToken = await _jwtService.GenerateRefreshToken(existingUser.UserId.ToString(), roleName)
            };
        }
    }
}
