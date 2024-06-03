using MediatR;
using NextIntern.Application.Common.Interfaces;
using NextIntern.Domain.IRepositories;

namespace NextIntern.Application.Auth.SignIn
{
    public class SignInQueryHandler : IRequestHandler<SignInQuery, string>
    {
        private readonly IInternRepository _internRepository;
        private readonly IJwtService _jwtService;

        public SignInQueryHandler(IInternRepository internRepository, IJwtService jwtService)
        {
            _internRepository = internRepository;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(SignInQuery request, CancellationToken cancellationToken)
        {
            var existingIntern = await _internRepository.FindAsync(i => i.Username == request.Username);

            if (existingIntern == null || !BCrypt.Net.BCrypt.Verify(request.Password, existingIntern.Password))
            {
                throw new Exception("Invalid username or password.");
            }

            string token = _jwtService.CreateToken(existingIntern.InternId.ToString(), existingIntern.Role.RoleName);

            return token;
        }
    }
}
