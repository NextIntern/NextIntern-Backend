using System;
using System.Collections.Generic;

namespace NextIntern.Domain.Entities;

public partial class Campaign
{
    public Guid CampaignId { get; set; }

    public int Id { get; set; }

    public string CampaignName { get; set; } = null!;

    public Guid? UniversityId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public virtual ICollection<CampaignEvaluation> CampaignEvaluations { get; set; } = new List<CampaignEvaluation>();

    public virtual ICollection<CampaignQuestion> CampaignQuestions { get; set; } = new List<CampaignQuestion>();

    public virtual ICollection<Intern> Interns { get; set; } = new List<Intern>();

    public virtual University? University { get; set; }
}
