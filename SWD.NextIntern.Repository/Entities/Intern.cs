namespace SWD.NextIntern.Repository.Entities;

public partial class Intern
{
    public Guid InternId { get; set; }

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

    public virtual Campaign? Campaign { get; set; }

    public virtual ICollection<CampaignQuestionResponse> CampaignQuestionResponses { get; set; } = new List<CampaignQuestionResponse>();

    public virtual ICollection<InternEvaluation> InternEvaluations { get; set; } = new List<InternEvaluation>();

    public virtual Staff? Mentor { get; set; }

    public virtual Role? Role { get; set; }
}
