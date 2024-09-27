using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Auth.SignIn
{
    public class SignInQuery : IRequest<TokenResponse>, IQuery
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
