using NextIntern.Application.Auth.ForgotPassword;
using NextIntern.Service.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextIntern.Application.Auth.ResetPassword
{
    public class ResetPasswordQueryHandler
    {
        public async Task<TokenResponse> Handle(ForgotPasswordQuery request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
