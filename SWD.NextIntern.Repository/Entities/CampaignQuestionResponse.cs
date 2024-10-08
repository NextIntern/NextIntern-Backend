﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    [JsonIgnore]
    public virtual CampaignQuestion? CampaignQuestion { get; set; }

    [JsonIgnore]
    public virtual User? Intern { get; set; }
}
