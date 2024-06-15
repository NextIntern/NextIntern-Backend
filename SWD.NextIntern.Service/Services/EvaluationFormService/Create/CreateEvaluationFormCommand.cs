using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Service.Services.EvaluationFormService.Create
{
    public class CreateEvaluationFormCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public CreateEvaluationFormCommand(Guid universityId, bool isActive, DateTime? createDate, DateTime? modifyDate, DateTime? deleteDate)
        {
            UniversityId = universityId;
            IsActive = isActive;
            CreateDate = createDate;
            ModifyDate = modifyDate;
            DeleteDate = deleteDate;
        }

        public Guid UniversityId { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public DateTime? CreateDate { get; set; }

        [JsonIgnore]
        public DateTime? ModifyDate { get; set; }

        [JsonIgnore]
        public DateTime? DeleteDate { get; set; }
    }
}
