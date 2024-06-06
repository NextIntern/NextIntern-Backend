using MediatR;
using NextIntern.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextIntern.Application.Auth.ResetPassword
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
