﻿namespace SWD.NextIntern.Repository.Entities;

public partial class InternEvaluation
{
    public Guid InternEvaluationId { get; set; }

    public int Id { get; set; }

    public Guid? InternId { get; set; }

    public Guid? CampaignEvaluationId { get; set; }

    public string? Feedback { get; set; }

    public virtual CampaignEvaluation? CampaignEvaluation { get; set; }

    public virtual Intern? Intern { get; set; }

    public virtual ICollection<InternEvaluationCriterion> InternEvaluationCriteria { get; set; } = new List<InternEvaluationCriterion>();
}
