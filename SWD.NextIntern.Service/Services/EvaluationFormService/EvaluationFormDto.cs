using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Service.Common.Mappings;

public class EvaluationFormDto : IMapFrom<EvaluationForm>
{
    public Guid EvaluationFormId { get; set; }
    public University? University { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public ICollection<FormCriteriaResponse> FormCriteriaResponse { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<FormCriterion, FormCriteriaResponse>();
        profile.CreateMap<EvaluationForm, EvaluationFormDto>()
            .ForMember(dest => dest.FormCriteriaResponse, opt => opt.MapFrom(src => src.FormCriteria));

    }
}

public class FormCriteriaResponse
{
    public string? Name { get; set; }

    public string? Guide { get; set; }

    public int MinScore { get; set; }

    public int MaxScore { get; set; }
}