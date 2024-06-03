using NextIntern.Application.Common.Interfaces;
using NextIntern.Domain.Entities;
using NextIntern.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextIntern.Application.Auth.SignUp
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

        public async Task<string> Handle(SignUpCommand request, CancellationToken cancellationToken)
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

            return _jwtService.CreateToken(newIntern.InternId.ToString(), newIntern.Role.RoleName);
        }
    }
}
