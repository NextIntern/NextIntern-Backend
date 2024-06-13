
using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.IRepositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Auth.SignUp
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, TokenResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IJwtService _jwtService;

        public SignUpCommandHandler(IUserRepository userRepository, IJwtService jwtService, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _roleRepository = roleRepository;
        }

        public async Task<TokenResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var existingIntern = await _userRepository.FindAsync(i => i.Username.Equals(request.Username));
            Role userRole = await _roleRepository.FindAsync(r => r.RoleName.Equals("User"));

            if (existingIntern != null)
            {
                throw new Exception("Username already taken.");
            }

            if (await _userRepository.FindAsync(i => i.Email.Equals(request.Email)) != null)
            {
                throw new Exception("Email already taken.");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newIntern = new User
            {
                Username = request.Username,
                Password = hashedPassword,
                FullName = request.FullName,
                Email = request.Email,
                Gender = request.Gender,
                Telephone = request.Telephone,
                Dob = request.Dob,
                Address = request.Address,
                Role = userRole
            };

            _userRepository.Add(newIntern);

            await _userRepository.UnitOfWork.SaveChangesAsync();

            return new TokenResponse
            {
                AccessToken = await _jwtService.CreateToken(newIntern.UserId.ToString(), newIntern.Role.RoleName),
                RefreshToken = await _jwtService.GenerateRefreshToken(newIntern.UserId.ToString(), newIntern.Role.RoleName)
            };
        }
    }
}
