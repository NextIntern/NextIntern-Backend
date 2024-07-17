using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Service.Services.UniversityService.Create
{
    public class CreateUniversityCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public CreateUniversityCommand(string universityName, string address, string phone, DateTime? createDate, DateTime? modifyDate, DateTime? deleteDate, string? imgUrl)
        {
            UniversityName = universityName;
            Address = address;
            Phone = phone;
            CreateDate = createDate;
            ModifyDate = modifyDate;
            DeleteDate = deleteDate;
            ImgUrl = imgUrl;
        }

        public string UniversityName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        [JsonIgnore]
        public DateTime? CreateDate { get; set; }

        [JsonIgnore]
        public DateTime? ModifyDate { get; set; }

        [JsonIgnore]
        public DateTime? DeleteDate { get; set; }
        public string? ImgUrl { get; set; }
    }
}
