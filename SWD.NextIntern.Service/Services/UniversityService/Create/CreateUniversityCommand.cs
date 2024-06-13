using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Service.Services.UniversityService.Create
{
    public class CreateUniversityCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public CreateUniversityCommand(string universityName, string address, string phone, DateOnly? createDate, DateOnly? modifyDate, DateOnly? deleteDate)
        {
            UniversityName = universityName;
            Address = address;
            Phone = phone;
            CreateDate = createDate;
            ModifyDate = modifyDate;
            DeleteDate = deleteDate;
        }

        public string UniversityName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        [JsonIgnore]
        public DateOnly? CreateDate { get; set; }

        [JsonIgnore]
        public DateOnly? ModifyDate { get; set; }

        [JsonIgnore]
        public DateOnly? DeleteDate { get; set; }
    }
}
