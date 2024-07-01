using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Repository.Entities;

public partial class FormCriterion
{
    public Guid FormCriteriaId { get; set; }

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Guide { get; set; }

    public int? MinScore { get; set; }

    public int? MaxScore { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? EvaluationFormId { get; set; }

    public virtual EvaluationForm? EvaluationForm { get; set; }

    [JsonIgnore]
    public virtual ICollection<InternEvaluationCriterion> InternEvaluationCriteria { get; set; } = new List<InternEvaluationCriterion>();
}
