
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
        public string Id { get; set; }

        //[JsonIgnore]
        //public string Username { get; set; }

        public string Fullname { get; set; }

        public DateOnly? Dob { get; set; }

        public string Gender { get; set; }

        public string Telephone { get; set; }

        //[JsonIgnore]
        //public string Email { get; set; }

        public string Address { get; set; }

        public string MenterUsername { get; set; }

        public string CampaignId { get; set; }

        public string RoleName { get; set; }

        [JsonIgnore]
        public DateTime? CreateDate { get; set; }

        [JsonIgnore]
        public DateTime? ModifyDate { get; set; }

        [JsonIgnore]
        public DateTime? DeleteDate { get; set; }
    }
}
