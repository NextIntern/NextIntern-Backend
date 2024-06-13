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

    public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();

    public virtual ICollection<EvaluationForm> EvaluationForms { get; set; } = new List<EvaluationForm>();
}
