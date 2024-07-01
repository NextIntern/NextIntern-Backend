using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Service.Common.Mappings;

public class FormCriteriaDto : IMapFrom<FormCriterion>
{
    public Guid FormCriteriaId { get; set; }

    public string? Name { get; set; }

    public string? Guide { get; set; }

    public int MinScore { get; set; }

    public int MaxScore { get; set; }

    public DateTime? DeletedDate { get; set; }

    //public string? EvaluationFormId { get; set; }

    //public string? EvaluationFromName { get; set; }

    public EvaluationForm? EvaluationForm { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<FormCriterion, FormCriteriaDto>();
    }
}