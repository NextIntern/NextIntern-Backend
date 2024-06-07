using System;
using System.Collections.Generic;

namespace SWD.NextIntern.Repository.Entities;

public partial class CampaignQuestionResponse
{
    public Guid CampaignQuestionResponseId { get; set; }

    public int Id { get; set; }

    public Guid? CampaignQuestionId { get; set; }

    public Guid? InternId { get; set; }

    public string? Response { get; set; }

    public int? Rating { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual CampaignQuestion? CampaignQuestion { get; set; }

    public virtual Intern? Intern { get; set; }
}
