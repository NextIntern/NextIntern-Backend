using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.InternEvaluationService.Create
{
    public class CreateInternEvaluationCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public string? InternId { get; set; }
        public string? CampaignEvaluationId { get; set; }
        public string? Feedback { get; set; }

        public CreateInternEvaluationCommand(string? internId, string? campaignEvaluationId, string? feedback)
        {
            InternId = internId;
            CampaignEvaluationId = campaignEvaluationId;
            Feedback = feedback;
        }
    }
}
