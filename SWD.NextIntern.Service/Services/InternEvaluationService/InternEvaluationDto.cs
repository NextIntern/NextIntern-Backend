using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Service.Common.Mappings;

namespace SWD.NextIntern.Service.Services.InternEvaluationService
{
    public class InternEvaluationDto : IMapFrom<InternEvaluation>
    {
        public Guid? InternId { get; set; }
        public Guid? CampaignEvaluationId { get; set; }
        public string? Feedback { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<InternEvaluation, InternEvaluationDto>();
        }
    }
}
