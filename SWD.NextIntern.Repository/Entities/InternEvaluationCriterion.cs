using System;
using System.Collections.Generic;

namespace SWD.NextIntern.Repository.Entities;

public partial class InternEvaluationCriterion
{
    public Guid InternEvaluationCriteriaId { get; set; }

    public int Id { get; set; }

    public Guid? InternEvaluationId { get; set; }

    public Guid? FormCriteriaId { get; set; }

    public int? Score { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual FormCriterion? FormCriteria { get; set; }

    public virtual InternEvaluation? InternEvaluation { get; set; }
}
