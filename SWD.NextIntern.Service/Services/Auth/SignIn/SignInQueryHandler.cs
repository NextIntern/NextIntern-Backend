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

        public SignInQueryHandler(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<TokenResponse> Handle(SignInQuery request, CancellationToken cancellationToken)
        {
            var existingIntern = await _userRepository.FindAsync(i => i.Username.Equals(request.Username));

            if (existingIntern == null || !BCrypt.Net.BCrypt.Verify(request.Password, existingIntern.Password))
            {
                throw new Exception("Invalid username or password.");
            }

            return new TokenResponse
            {
                AccessToken = await _jwtService.CreateToken(existingIntern.UserId.ToString(), existingIntern.Role.RoleName),
                RefreshToken = await _jwtService.GenerateRefreshToken(existingIntern.UserId.ToString(), existingIntern.Role.RoleName)
            };
        }
    }
}
