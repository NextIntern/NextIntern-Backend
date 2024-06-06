using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextIntern.Application.Auth.ForgotPassword
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
