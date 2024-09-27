using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Service.Services.FormCriteriaService.CreateList
{
    public class CreateListFormCriteriaCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public List<FormCriteriaRequest> ListFormCriteria { get; set; }

        [JsonIgnore]
        public DateTime? DeleteDate { get; set; }

        public string EvaluationFormId { get; set; }

        public CreateListFormCriteriaCommand(List<FormCriteriaRequest> listFormCriteria, DateTime? deleteDate, string evaluationFormId)
        {
            ListFormCriteria = listFormCriteria;
            DeleteDate = deleteDate;
            EvaluationFormId = evaluationFormId;
        }
    }

    public class FormCriteriaRequest
    {
        public string FormCriteriaName { get; set; }

        public string Guide { get; set; }

        public int MinScore { get; set; }

        public int MaxScore { get; set; }
    }
}
