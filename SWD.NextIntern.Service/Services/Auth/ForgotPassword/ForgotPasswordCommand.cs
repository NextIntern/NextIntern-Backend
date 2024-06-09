
namespace SWD.NextIntern.Service.Auth.ForgotPassword
{
    public class ForgotPasswordQuery
    {
        public string Email { get; set; }

        public ForgotPasswordQuery(string email)
        {
            Email = email;
        }
    }
}
