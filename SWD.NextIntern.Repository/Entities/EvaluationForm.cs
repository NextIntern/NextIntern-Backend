using System;
using System.Collections.Generic;

namespace SWD.NextIntern.Repository.Entities;

public partial class EvaluationForm
{
    public Guid EvaluationFormId { get; set; }

    public int Id { get; set; }

    public Guid? UniversityId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual ICollection<FormCriterion> FormCriteria { get; set; } = new List<FormCriterion>();

    public virtual University? University { get; set; }
}
