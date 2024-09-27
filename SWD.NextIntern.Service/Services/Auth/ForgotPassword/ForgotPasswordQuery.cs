
using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;

namespace SWD.NextIntern.Service.Auth.ForgotPassword
{
    public class ForgotPasswordQuery : IRequest<string>, IQuery
    {
        public string Email { get; set; }

        public ForgotPasswordQuery(string email)
        {
            Email = email;
        }
    }
}
