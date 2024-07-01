
using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.IRepositories;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Linq.Expressions;
using System.Net;

namespace SWD.NextIntern.Service.InternService.Create
{
    public class CreateInternCommandHandler : IRequestHandler<CreateInternCommand, TokenResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IRoleRepository _roleRepository;

        public CreateInternCommandHandler(IUserRepository userRepository, IJwtService jwtService, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _roleRepository = roleRepository;
        }
        public async Task<TokenResponse> Handle(CreateInternCommand request, CancellationToken cancellationToken)
        {
            var existingIntern = await _userRepository.FindAsync(i => i.Username.Equals(request.Username));
            var existEmailIntern = await _userRepository.FindAsync(i => i.Email.Equals(request.Email));
            Expression<Func<Role, bool>> queryFilter = (Role r) => r.RoleName.Equals(request.RoleName) && r.DeletedDate == null;
            var role = await _roleRepository.FindAsync(queryFilter, cancellationToken);

            if (existingIntern != null)
            {
                throw new Exception("Username already has taken.");
            }

            if (existEmailIntern != null)
            {
                throw new Exception("Email already has taken.");
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
                RoleId = role.RoleId
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
