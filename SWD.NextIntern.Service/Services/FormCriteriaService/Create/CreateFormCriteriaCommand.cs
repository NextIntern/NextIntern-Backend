using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Service.Services.FormCriteriaService.Create
{
    public class CreateFormCriteriaCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public CreateFormCriteriaCommand(string formCriteriaName, string guide, int minScore, int maxScore, DateTime? deleteDate, string evaluationFormId)
        {
            FormCriteriaName = formCriteriaName;
            Guide = guide;
            MinScore = minScore;
            MaxScore = maxScore;
            DeleteDate = deleteDate;
            EvaluationFormId = evaluationFormId;
        }

        public string FormCriteriaName { get; set; }

        public string Guide { get; set; }

        public int MinScore { get; set; }

        public int MaxScore { get; set; }

        [JsonIgnore]
        public DateTime? DeleteDate { get; set; }

        public string EvaluationFormId { get; set; }
    }
}
