using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.InternEvaluationService.Update
{
    public class UpdateInternEvaluationCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public string Id { get; set; }
        public string? InternId { get; set; }
        public string? CampaignEvaluationId { get; set; }
        public string? Feedback { get; set; }

        public UpdateInternEvaluationCommand(string? internId, string? campaignEvaluationId, string? feedback, string id)
        {
            InternId = internId;
            CampaignEvaluationId = campaignEvaluationId;
            Feedback = feedback;
            Id = id;
        }
    }
}
