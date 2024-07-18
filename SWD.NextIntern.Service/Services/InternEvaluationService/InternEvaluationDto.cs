using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.Common.Mappings;

namespace SWD.NextIntern.Service.Services.InternEvaluationService
{
    public class InternEvaluationDto : IMapFrom<InternEvaluation>
    {
        public Guid? InternEvaluationId { get; set; }
        public Guid? InternId { get; set; }
        public string InternName { get; set; }
        //public User Intern { get; set; }
        public Guid? CampaignEvaluationId { get; set; }
        public string? Feedback { get; set; }
        public string UniversityName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<InternEvaluation, InternEvaluationDto>()
                .ForMember(dest => dest.InternName, opt => opt.MapFrom(src => src.Intern != null ? src.Intern.FullName : string.Empty))
                .ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src => src.CampaignEvaluation.Campaign.University.UniversityName));
        }
    }
}
