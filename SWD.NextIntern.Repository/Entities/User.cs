using System;
using System.Collections.Generic;

namespace SWD.NextIntern.Repository.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly? Dob { get; set; }

    public string? Gender { get; set; }

    public string? Telephone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public Guid? MentorId { get; set; }

    public Guid? CampaignId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public Guid? RoleId { get; set; }

    public int? Otp { get; set; }

    public DateOnly? OtpExpired { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? ImgUrl { get; set; }

    public Guid? UniversityId { get; set; }

    public int? State { get; set; } //0=Failed, 1=During, 2=Completed

    public virtual Campaign? Campaign { get; set; }

    [JsonIgnore]
    public virtual ICollection<CampaignQuestionResponse> CampaignQuestionResponses { get; set; } = new List<CampaignQuestionResponse>();

    [JsonIgnore]
    public virtual ICollection<InternEvaluation> InternEvaluations { get; set; } = new List<InternEvaluation>();

    [JsonIgnore]
    public virtual ICollection<User> InverseMentor { get; set; } = new List<User>();

    [JsonIgnore]
    public virtual User? Mentor { get; set; }

    [JsonIgnore]
    public virtual Role? Role { get; set; }
}
