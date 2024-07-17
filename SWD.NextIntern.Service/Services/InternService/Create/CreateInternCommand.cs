
using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.InternService.Create
{
    public class CreateInternCommand : IRequest<TokenResponse>, ICommand
    {
        public CreateInternCommand(string username, string password, string confirmedPassword, string fullName, string email, string gender, string telephone, DateOnly? dob, string roleName, string address, string imgUrl)
        {
            Username = username;
            Password = password;
            ConfirmedPassword = confirmedPassword;
            Fullname = fullName;
            Email = email;
            Gender = gender;
            Telephone = telephone;
            Dob = dob;
            RoleName = roleName;
            Address = address;
            ImgUrl = imgUrl;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Telephone { get; set; }
        public DateOnly? Dob { get; set; }
        public string RoleName { get; set; }
        public string Address { get; set; }
        public string ImgUrl { get; set; }
    }
}
