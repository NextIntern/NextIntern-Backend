using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Repository.Entities;

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

    public DateTime? DeletedDate { get; set; }

    public virtual ICollection<CampaignEvaluation> CampaignEvaluations { get; set; } = new List<CampaignEvaluation>();

    public virtual ICollection<CampaignQuestion> CampaignQuestions { get; set; } = new List<CampaignQuestion>();

    public virtual University? University { get; set; }

    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
