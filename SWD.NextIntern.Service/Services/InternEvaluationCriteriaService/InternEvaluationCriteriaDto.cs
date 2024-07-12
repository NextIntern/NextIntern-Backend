using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Service.Common.Mappings;
using System.Text.Json.Serialization;

public class InternEvaluationCriteriaDto : IMapFrom<InternEvaluationCriterion>
{
    public Guid InternEvaluationCriteriaId { get; set; }

    public string Id { get; set; }

    public InternEvaluation InternEvaluation { get; set; }

    public FormCriterion FormCriteria { get; set; }

    public int Score { get; set; }

    [JsonIgnore]
    public DateTime? DeletedDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<InternEvaluationCriterion, InternEvaluationCriteriaDto>();
    }
}