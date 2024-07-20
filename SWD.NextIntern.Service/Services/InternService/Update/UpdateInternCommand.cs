
using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;
using System.Numerics;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Service.Services.InternService.Update
{
    public class UpdateInternCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public UpdateInternCommand(string id, string fullname, DateOnly? dob, string gender, string telephone, string address, string campaignId, string roleName, DateTime? createDate, DateTime? modifyDate, DateTime? deleteDate, string imgUrl, string? universityId, string email)
        {
            Id = id;
            Fullname = fullname;
            Dob = dob;
            Gender = gender;
            Telephone = telephone;
            Address = address;
            //MenterUsername = menterUsername;
            CampaignId = campaignId;
            RoleName = roleName;
            CreateDate = createDate;
            ModifyDate = modifyDate;
            DeleteDate = deleteDate;
            ImgUrl = imgUrl;
            UniversityId = universityId;
            Email = email;
        }

        public string Id { get; set; }

        //[JsonIgnore]
        //public string Username { get; set; }

        public string Fullname { get; set; }

        public DateOnly? Dob { get; set; }

        public string Gender { get; set; }

        public string Telephone { get; set; }

        //[JsonIgnore]
        public string Email { get; set; }

        public string Address { get; set; }

        //public string MenterUsername { get; set; }

        public string CampaignId { get; set; }

        public string RoleName { get; set; }

        [JsonIgnore]
        public DateTime? CreateDate { get; set; }

        [JsonIgnore]
        public DateTime? ModifyDate { get; set; }

        [JsonIgnore]
        public DateTime? DeleteDate { get; set; }

        public string ImgUrl { get; set; }

        public string? UniversityId { get; set; }
    }
}
