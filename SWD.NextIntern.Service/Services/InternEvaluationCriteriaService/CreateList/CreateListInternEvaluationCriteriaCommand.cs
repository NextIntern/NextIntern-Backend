using MediatR;
using Newtonsoft.Json;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.CreateList
{
    public class CreateListInternEvaluationCriteriaCommand : IRequest<ResponseObject<string>>
    {
        public List<InternEvaluationCriteriaRequest> InternEvaluationCriterias { get; set; }
        public string? InternId { get; set; }
        public string? CampaignEvaluationId { get; set; }

        public CreateListInternEvaluationCriteriaCommand(List<InternEvaluationCriteriaRequest> internEvaluationCriterias, string? internId, string? campaignEvaluationId)
        {
            InternEvaluationCriterias = internEvaluationCriterias;
            InternId = internId;
            CampaignEvaluationId = campaignEvaluationId;
        }
    }

    public class InternEvaluationCriteriaRequest
    {
        public string? FormCriteriaId { get; set; }
        public int? Score { get; set; }
    }
}
