﻿
using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.InternEvaluationCriteriaService.Create
{
    public class CreateInternEvaluationCriteriaCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public CreateInternEvaluationCriteriaCommand(string? internEvaluationId, string? fromCriteriaId, int? score)
        {
            InternEvaluationId = internEvaluationId;
            FromCriteriaId = fromCriteriaId;
            Score = score;
        }

        public string? InternEvaluationId { get; set; }
        public string? FromCriteriaId { get; set; }
        public int? Score {  get; set; }
    }
}
