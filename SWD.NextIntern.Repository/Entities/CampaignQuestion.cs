using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Repository.Entities;

public partial class CampaignQuestion
{
    public Guid CampaignQuestionId { get; set; }

    public int Id { get; set; }

    public Guid? CampaignId { get; set; }

    public string Question { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    [JsonIgnore]
    public virtual Campaign? Campaign { get; set; }

    [JsonIgnore]
    public virtual ICollection<CampaignQuestionResponse> CampaignQuestionResponses { get; set; } = new List<CampaignQuestionResponse>();
}
