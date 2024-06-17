using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.CampaignEvaluationService.Update
{
    public class UpdateCampaignEvaluationCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public string Id { get; set; }

        public string? CampaignId { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public UpdateCampaignEvaluationCommand(string? campaignId, DateOnly? startDate, DateOnly? endDate, string id)
        {
            CampaignId = campaignId;
            StartDate = startDate;
            EndDate = endDate;
            Id = id;
        }
    }
}
