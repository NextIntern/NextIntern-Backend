using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.Repository.Entities;

public partial class University
{
    public Guid UniversityId { get; set; }

    public int Id { get; set; }

    public string UniversityName { get; set; } = null!;

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? ImgUrl { get; set; }

    public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();

    [JsonIgnore]
    public virtual ICollection<EvaluationForm> EvaluationForms { get; set; } = new List<EvaluationForm>();
}
