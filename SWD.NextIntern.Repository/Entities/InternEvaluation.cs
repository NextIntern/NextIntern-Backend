using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Repository.Entities;

public partial class InternEvaluation
{
    public Guid InternEvaluationId { get; set; }

    public int Id { get; set; }

    public Guid? InternId { get; set; }

    public Guid? CampaignEvaluationId { get; set; }

    public string? Feedback { get; set; }

    public DateTime? DeletedDate { get; set; }

    [JsonIgnore]
    public virtual CampaignEvaluation? CampaignEvaluation { get; set; }

    //[JsonIgnore]
    public virtual User? Intern { get; set; }

    [JsonIgnore]
    public virtual ICollection<InternEvaluationCriterion> InternEvaluationCriteria { get; set; } = new List<InternEvaluationCriterion>();
}
