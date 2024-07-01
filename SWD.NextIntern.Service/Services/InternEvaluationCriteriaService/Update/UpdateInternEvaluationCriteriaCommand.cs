
using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;
using System.Numerics;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.Update
{
    public class UpdateInternEvaluationCriteriaCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public UpdateInternEvaluationCriteriaCommand(string internEvaluationCriteriaId, string internEvaluationId, string fromCriteriaId, int score)
        {
            InternEvaluationCriteriaId = internEvaluationCriteriaId;
            InternEvaluationId = internEvaluationId;
            FromCriteriaId = fromCriteriaId;
            Score = score;
        }

        public string InternEvaluationCriteriaId { get; set; }
        public string InternEvaluationId { get; set; }
        public string FromCriteriaId { get; set; }
        public int Score { get; set; }
    }
}
