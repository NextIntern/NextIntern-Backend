using System;
using System.Collections.Generic;

namespace SWD.NextIntern.Repository.Entities;

public partial class EvaluationFormDetail
{
    public Guid EvaluationFormDetailId { get; set; }

    public int Id { get; set; }

    public Guid? EvaluationFormId { get; set; }

    public Guid? FormCriteriaId { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual EvaluationForm? EvaluationForm { get; set; }

    public virtual FormCriterion? FormCriteria { get; set; }
}
