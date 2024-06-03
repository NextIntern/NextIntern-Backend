using MediatR;
using NextIntern.Application.Common.Interfaces;

namespace NextIntern.Application.Auth.SignIn
{
    public class SignInQuery : IRequest<string>, ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public SignInQuery(string password, string email)
        {
            Password = password;
            Username = email;
        }

    }
}
