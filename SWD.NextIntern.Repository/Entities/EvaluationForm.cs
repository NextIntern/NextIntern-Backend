namespace SWD.NextIntern.Repository.Entities;

public partial class EvaluationForm
{
    public Guid EvaluationFormId { get; set; }

    public int Id { get; set; }

    public Guid? UniversityId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public virtual ICollection<EvaluationFormDetail> EvaluationFormDetails { get; set; } = new List<EvaluationFormDetail>();

    public virtual University? University { get; set; }
}
