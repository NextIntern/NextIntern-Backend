using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Service.Common.Mappings;
using SWD.NextIntern.Service.Services.CampaignService;

namespace SWD.NextIntern.Service.Services.CampaignEvaluationService
{
    public class CampaignEvaluationDto : IMapFrom<CampaignEvaluation>
    {
        public Guid CampaignEvaluationId { get; set; }

        public Guid? CampaignId { get; set; }
        public string CampaignName { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CampaignEvaluation, CampaignEvaluationDto>()
                .ForMember(dest => dest.CampaignName, opt => opt.MapFrom(src => src.Campaign.CampaignName));
        }
    }
}
