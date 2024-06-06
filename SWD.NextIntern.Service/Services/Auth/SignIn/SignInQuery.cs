using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;

namespace SWD.NextIntern.Service.Auth.SignIn
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
