using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    [JsonIgnore]
    public virtual ICollection<FormCriterion> FormCriteria { get; set; } = new List<FormCriterion>();

    [JsonIgnore]
    public virtual University? University { get; set; }
}
