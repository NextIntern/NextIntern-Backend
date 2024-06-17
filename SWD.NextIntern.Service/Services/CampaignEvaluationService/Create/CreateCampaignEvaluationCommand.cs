using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.CampaignEvaluationService.Create
{
    public class CreateCampaignEvaluationCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public string? CampaignId { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public CreateCampaignEvaluationCommand(string? campaignId, DateOnly? startDate, DateOnly? endDate)
        {
            CampaignId = campaignId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
