
using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;
using System.Numerics;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Service.Services.FormCriteriaService.Update
{
    public class UpdateFormCriteriaCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public UpdateFormCriteriaCommand(string id, string formCriteriaName, string guide, int minScore, int maxScore, DateTime? deleteDate, string? evaluationFormId)
        {
            Id = id;
            FormCriteriaName = formCriteriaName;
            Guide = guide;
            MinScore = minScore;
            MaxScore = maxScore;
            DeleteDate = deleteDate;
            EvaluationFormId = evaluationFormId;
        }

        public string Id { get; set; }

        public string FormCriteriaName { get; set; }

        public string Guide { get; set; }

        public int MinScore { get; set; }

        public int MaxScore { get; set; }

        [JsonIgnore]
        public DateTime? DeleteDate { get; set; }

        public string? EvaluationFormId { get; set; }
    }
}
