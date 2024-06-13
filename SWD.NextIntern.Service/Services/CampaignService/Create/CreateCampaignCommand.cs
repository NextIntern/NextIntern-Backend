using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.CampaignService.Create
{
    public class CreateCampaignCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public string CampaignName { get; set; }

        public string? UniversityId { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public CreateCampaignCommand(string campaignName, string? universityId, DateOnly? startDate, DateOnly? endDate)
        {
            CampaignName = campaignName;
            UniversityId = universityId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
