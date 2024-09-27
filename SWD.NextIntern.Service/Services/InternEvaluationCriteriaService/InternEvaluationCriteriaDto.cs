using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Service.Common.Mappings;
using SWD.NextIntern.Service.Services.InternEvaluationService;
using System.Text.Json.Serialization;

public class InternEvaluationCriteriaDto : IMapFrom<InternEvaluationCriterion>
{
    public Guid InternEvaluationCriteriaId { get; set; }

    public string Id { get; set; }

    public InternEvaluationDto InternEvaluationDto { get; set; }

    public FormCriteriaDto FormCriteriaDto { get; set; }

    public int Score { get; set; }

    [JsonIgnore]
    public DateTime? DeletedDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<InternEvaluationCriterion, InternEvaluationCriteriaDto>()
            .ForMember(dest => dest.InternEvaluationDto, opt => opt.MapFrom(src => src.InternEvaluation))
            .ForMember(dest => dest.FormCriteriaDto, opt => opt.MapFrom(src => src.FormCriteria));
    }
}