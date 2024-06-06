using SWD.NextIntern.Service.Auth.ForgotPassword;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Auth.ResetPassword
{
    public class ResetPasswordQueryHandler
    {
        public async Task<TokenResponse> Handle(ForgotPasswordQuery request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
