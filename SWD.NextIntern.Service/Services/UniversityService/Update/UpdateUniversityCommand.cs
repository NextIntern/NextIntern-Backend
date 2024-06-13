
using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;
using System.Numerics;

namespace SWD.NextIntern.Service.Services.UniversityService.Update
{
    public class UpdateUniversityCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public string Id { get; set; }

        public string? UniversityId { get; set; }

        public string? UniversityName { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public DateOnly? ModifyDate { get; set; }

        public UpdateUniversityCommand(string? universityName, string address, string phone, DateOnly? modifyDate, string id)
        {
            UniversityName = universityName;
            Address = address;
            Phone = phone;
            ModifyDate = modifyDate;
            Id = id;
        }
    }
}
