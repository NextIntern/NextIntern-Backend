using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;

namespace SWD.NextIntern.Service.Auth.ResetPassword
{
    public class ResetPasswordQuery : IRequest<string>, IQuery
    {
        public string Email { get; set; }

        public ResetPasswordQuery(string email)
        {
            Email = email;
        }
    }
}
