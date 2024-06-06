using System;
using System.Collections.Generic;

namespace NextIntern.Domain.Entities;

public partial class CampaignQuestion
{
    public Guid CampaignQuestionId { get; set; }

    public int Id { get; set; }

    public Guid? CampaignId { get; set; }

    public string Question { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public virtual Campaign? Campaign { get; set; }

    public virtual ICollection<CampaignQuestionResponse> CampaignQuestionResponses { get; set; } = new List<CampaignQuestionResponse>();
}
