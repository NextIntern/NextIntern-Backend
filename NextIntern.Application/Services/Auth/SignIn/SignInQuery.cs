using MediatR;
using NextIntern.Application.Common.Interfaces;

namespace NextIntern.Application.Auth.SignIn
{
    public class SignInQuery : IRequest<string>, IQuery
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public SignInQuery(string username, string password)
        {
            Password = password;
            Username = username;
        }
    }
}
