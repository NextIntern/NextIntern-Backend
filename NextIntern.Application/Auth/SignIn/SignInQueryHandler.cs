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
            var intern = await _internRepository.FindAsync(i => i.Username == request.Username);

            if (intern is null)
            {
                return "";
            }

            bool isMatch = BCrypt.Net.BCrypt.Verify(intern.Password, request.Password);

            if (isMatch)
            {
                return _jwtService.CreateToken(intern.InternId.ToString(), intern.Role.RoleName);
            }

            return "";
        }

    }
}
