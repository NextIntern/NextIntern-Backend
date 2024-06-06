using System;
using System.Collections.Generic;

namespace NextIntern.Domain.Entities;

public partial class EvaluationFormDetail
{
    public Guid EvaluationFormDetailId { get; set; }

    public int Id { get; set; }

    public Guid? EvaluationFormId { get; set; }

    public Guid? FormCriteriaId { get; set; }

    public virtual EvaluationForm? EvaluationForm { get; set; }

    public virtual FormCriterion? FormCriteria { get; set; }
}
