
using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;
using System.Numerics;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Service.Services.UniversityService.Update
{
    public class UpdateUniversityCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public string Id { get; set; }

        [JsonIgnore]
        public string? UniversityId { get; set; }

        public string? UniversityName { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        [JsonIgnore]
        public DateTime? ModifyDate { get; set; }
        public string ImgUrl { get; set; }

        public UpdateUniversityCommand(string? universityName, string? universityId, string address, string phone, DateTime? modifyDate, string id, string imgUrl)
        {
            UniversityName = universityName;
            UniversityId = universityId;
            Address = address;
            Phone = phone;
            ModifyDate = modifyDate;
            Id = id;
            ImgUrl = imgUrl;
        }
    }
}
