using System;
using System.Collections.Generic;

namespace SWD.NextIntern.Repository.Entities;

public partial class CampaignEvaluation
{
    public Guid CampaignEvaluationId { get; set; }

    public int Id { get; set; }

    public Guid? CampaignId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual Campaign? Campaign { get; set; }

    public virtual ICollection<InternEvaluation> InternEvaluations { get; set; } = new List<InternEvaluation>();
}
