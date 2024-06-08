using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;

namespace SWD.NextIntern.Service.Services.CampaignService.Create
{
    public class CreateCampaignCommand : IRequest<string>, ICommand
    {
        public string CampaignName { get; set; }

        public Guid? UniversityId { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public CreateCampaignCommand(string campaignName, Guid? universityId, DateOnly? startDate, DateOnly? endDate)
        {
            CampaignName = campaignName;
            UniversityId = universityId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
