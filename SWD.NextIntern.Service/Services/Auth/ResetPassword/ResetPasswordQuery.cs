using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;

namespace SWD.NextIntern.Service.Auth.ResetPassword
{
    public class ResetPasswordQuery : IRequest<string>, IQuery
    {
        public string Email { get; set; }
        public string OTP { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        public ResetPasswordQuery(string email, string oTP, string newPassword, string confirmPassword)
        {
            Email = email;
            OTP = oTP;
            NewPassword = newPassword;
            ConfirmPassword = confirmPassword;
        }
    }
}
