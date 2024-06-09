
namespace SWD.NextIntern.Service.Auth.SignUp
{
    public class SignUpCommand
    {
        public SignUpCommand(string username, string password, string fullName, string email, string gender, string telephone, DateOnly? dob, string roleName, string address)
        {
            Username = username;
            Password = password;
            FullName = fullName;
            Email = email;
            Gender = gender;
            Telephone = telephone;
            Dob = dob;
            RoleName = roleName;
            Address = address;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Telephone { get; set; }
        public DateOnly? Dob { get; set; }
        public string RoleName { get; set; }
        public string Address { get; set; }
    }
}
