using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Service.Common.Mappings;

public class EvaluationFormDto : IMapFrom<EvaluationForm>
{
    public Guid EvaluationFormId { get; set; }

    //public Guid UniversityId { get; set; }

    public University? University { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EvaluationForm, EvaluationFormDto>();
    }
}