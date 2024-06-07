using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;

namespace SWD.NextIntern.Service.Services.CampaignService.Update
{
    public class UpdateCampaignCommand : IRequest<string>, ICommand
    {
        public string Id { get; set; }

        public string? UniversityId { get; set; }

        public string? CampaignName { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public UpdateCampaignCommand(string? campaignName, string? universityId, DateOnly? startDate, DateOnly? endDate, string id)
        {
            CampaignName = campaignName;
            UniversityId = universityId;
            StartDate = startDate;
            EndDate = endDate;
            Id = id;
        }
    }
}
