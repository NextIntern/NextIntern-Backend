﻿
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.IRepositories;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Auth.SignUp
{
    public class SignUpCommandHandler
    {
        private readonly IInternRepository _internRepository;
        private readonly IJwtService _jwtService;

        public SignUpCommandHandler(IInternRepository internRepository, IJwtService jwtService)
        {
            _internRepository = internRepository;
            _jwtService = jwtService;
        }

        public async Task<TokenResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var existingIntern = await _internRepository.FindAsync(i => i.Username == request.Username);

            if (existingIntern != null)
            {
                throw new Exception("Username already taken.");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newIntern = new Intern
            {
                Username = request.Username,
                Password = hashedPassword,
                FullName = request.FullName,
                Email = request.Email,
                Gender = request.Gender,
                Telephone = request.Telephone,
                Dob = request.Dob,
                Address = request.Address,
                Role = new Role { RoleName = "User" }
            };

            await _internRepository.AddAsync(newIntern);

            await _internRepository.SaveChangesAsync();

            return new TokenResponse
            {
                AccessToken = _jwtService.CreateToken(newIntern.InternId.ToString(), newIntern.Role.RoleName),
                RefreshToken = _jwtService.GenerateRefreshToken(newIntern.InternId.ToString(), newIntern.Role.RoleName)
            };

        }
    }
}
