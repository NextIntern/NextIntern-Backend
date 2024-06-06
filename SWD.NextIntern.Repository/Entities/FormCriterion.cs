namespace SWD.NextIntern.Repository.Entities;

public partial class FormCriterion
{
    public Guid FormCriteriaId { get; set; }

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Guide { get; set; }

    public int? MinScore { get; set; }

    public int? MaxScore { get; set; }

    public virtual ICollection<EvaluationFormDetail> EvaluationFormDetails { get; set; } = new List<EvaluationFormDetail>();

    public virtual ICollection<InternEvaluationCriterion> InternEvaluationCriteria { get; set; } = new List<InternEvaluationCriterion>();
}
