
using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;
using System.Numerics;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Service.Services.EvaluationFormService.Update
{
    public class UpdateEvaluationFormCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public UpdateEvaluationFormCommand(string id, string? universityId, bool isActive, DateTime? createDate, DateTime? modifyDate, DateTime? deleteDate)
        {
            Id = id;
            UniversityId = universityId;
            IsActive = isActive;
            CreateDate = createDate;
            ModifyDate = modifyDate;
            DeleteDate = deleteDate;
        }

        public string Id { get; set; }

        public string? UniversityId { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public DateTime? CreateDate { get; set; }

        [JsonIgnore]
        public DateTime? ModifyDate { get; set; }

        [JsonIgnore]
        public DateTime? DeleteDate { get; set; }
    }
}
