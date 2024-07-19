using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Common;
using SWD.NextIntern.Repository.Entities;

namespace SWD.NextIntern.Repository.Persistence;

public partial class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Campaign> Campaigns { get; set; }

    public virtual DbSet<CampaignEvaluation> CampaignEvaluations { get; set; }

    public virtual DbSet<CampaignQuestion> CampaignQuestions { get; set; }

    public virtual DbSet<CampaignQuestionResponse> CampaignQuestionResponses { get; set; }

    public virtual DbSet<EvaluationForm> EvaluationForms { get; set; }

    public virtual DbSet<FormCriterion> FormCriteria { get; set; }

    public virtual DbSet<InternEvaluation> InternEvaluations { get; set; }

    public virtual DbSet<InternEvaluationCriterion> InternEvaluationCriteria { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    public virtual DbSet<User> Users { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseNpgsql("Host=database.nextintern.tech;Database=nextintern;Username=root;Password=iumaycauratnhiu");

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                var deletedDateProperty = entry.Entity.GetType().GetProperty("DeletedDate");
                if (deletedDateProperty is not null)
                {
                    deletedDateProperty.SetValue(entry.Entity, DateTime.Now);
                }
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Campaign>(entity =>
        {
            entity.HasKey(e => e.CampaignId).HasName("campaign_pkey");

            entity.ToTable("campaign");

            entity.Property(e => e.CampaignId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("campaign_id");
            entity.Property(e => e.CampaignName)
                .HasMaxLength(255)
                .HasColumnName("campaign_name");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_date");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.ModifyDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modify_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.UniversityId).HasColumnName("university_id");

            entity.HasOne(d => d.University).WithMany(p => p.Campaigns)
                .HasForeignKey(d => d.UniversityId)
                .HasConstraintName("campaign_university_id_fkey");
        });

        modelBuilder.Entity<CampaignEvaluation>(entity =>
        {
            entity.HasKey(e => e.CampaignEvaluationId).HasName("campaign_evaluation_pkey");

            entity.ToTable("campaign_evaluation");

            entity.Property(e => e.CampaignEvaluationId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("campaign_evaluation_id");
            entity.Property(e => e.CampaignId).HasColumnName("campaign_id");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_date");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.Campaign).WithMany(p => p.CampaignEvaluations)
                .HasForeignKey(d => d.CampaignId)
                .HasConstraintName("campaign_evaluation_campaign_id_fkey");
        });

        modelBuilder.Entity<CampaignQuestion>(entity =>
        {
            entity.HasKey(e => e.CampaignQuestionId).HasName("campaign_question_pkey");

            entity.ToTable("campaign_question");

            entity.Property(e => e.CampaignQuestionId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("campaign_question_id");
            entity.Property(e => e.CampaignId).HasColumnName("campaign_id");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_date");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.ModifyDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modify_date");
            entity.Property(e => e.Question).HasColumnName("question");

            entity.HasOne(d => d.Campaign).WithMany(p => p.CampaignQuestions)
                .HasForeignKey(d => d.CampaignId)
                .HasConstraintName("campaign_question_campaign_id_fkey");
        });

        modelBuilder.Entity<CampaignQuestionResponse>(entity =>
        {
            entity.HasKey(e => e.CampaignQuestionResponseId).HasName("campaign_question_response_pkey");

            entity.ToTable("campaign_question_response");

            entity.Property(e => e.CampaignQuestionResponseId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("campaign_question_response_id");
            entity.Property(e => e.CampaignQuestionId).HasColumnName("campaign_question_id");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_date");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.InternId).HasColumnName("intern_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Response).HasColumnName("response");

            entity.HasOne(d => d.CampaignQuestion).WithMany(p => p.CampaignQuestionResponses)
                .HasForeignKey(d => d.CampaignQuestionId)
                .HasConstraintName("campaign_question_response_campaign_question_id_fkey");

            entity.HasOne(d => d.Intern).WithMany(p => p.CampaignQuestionResponses)
                .HasForeignKey(d => d.InternId)
                .HasConstraintName("campaign_question_response_intern_id_fkey");
        });

        modelBuilder.Entity<EvaluationForm>(entity =>
        {
            entity.HasKey(e => e.EvaluationFormId).HasName("evaluation_form_pkey");

            entity.ToTable("evaluation_form");

            entity.Property(e => e.EvaluationFormId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("evaluation_form_id");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_date");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ModifyDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modify_date");
            entity.Property(e => e.UniversityId).HasColumnName("university_id");

            entity.HasOne(d => d.University).WithMany(p => p.EvaluationForms)
                .HasForeignKey(d => d.UniversityId)
                .HasConstraintName("evaluation_form_university_id_fkey");
        });

        modelBuilder.Entity<FormCriterion>(entity =>
        {
            entity.HasKey(e => e.FormCriteriaId).HasName("form_criteria_pkey");

            entity.ToTable("form_criteria");

            entity.Property(e => e.FormCriteriaId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("form_criteria_id");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_date");
            entity.Property(e => e.EvaluationFormId).HasColumnName("evaluation_form_id");
            entity.Property(e => e.Guide).HasColumnName("guide");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.MaxScore).HasColumnName("max_score");
            entity.Property(e => e.MinScore).HasColumnName("min_score");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.EvaluationForm).WithMany(p => p.FormCriteria)
                .HasForeignKey(d => d.EvaluationFormId)
                .HasConstraintName("evaluation_form_id_fkey");
        });

        modelBuilder.Entity<InternEvaluation>(entity =>
        {
            //entity.HasMany(e => e.InternEvaluationCriteria)
            //      .WithOne(c => c.InternEvaluation)
            //      .OnDelete(DeleteBehavior.Cascade);

            entity.HasKey(e => e.InternEvaluationId).HasName("intern_evaluation_pkey");

            entity.ToTable("intern_evaluation");

            entity.Property(e => e.InternEvaluationId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("intern_evaluation_id");
            entity.Property(e => e.CampaignEvaluationId).HasColumnName("campaign_evaluation_id");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_date");
            entity.Property(e => e.Feedback).HasColumnName("feedback");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.InternId).HasColumnName("intern_id");

            entity.HasOne(d => d.CampaignEvaluation).WithMany(p => p.InternEvaluations)
                .HasForeignKey(d => d.CampaignEvaluationId)
                .HasConstraintName("intern_evaluation_campaign_evaluation_id_fkey");

            entity.HasOne(d => d.Intern).WithMany(p => p.InternEvaluations)
                .HasForeignKey(d => d.InternId)
                .HasConstraintName("intern_evaluation_intern_id_fkey");
        });

        modelBuilder.Entity<InternEvaluationCriterion>(entity =>
        {
            entity.HasKey(e => e.InternEvaluationCriteriaId).HasName("intern_evaluation_criteria_pkey");

            entity.ToTable("intern_evaluation_criteria");

            entity.Property(e => e.InternEvaluationCriteriaId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("intern_evaluation_criteria_id");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_date");
            entity.Property(e => e.FormCriteriaId).HasColumnName("form_criteria_id");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.InternEvaluationId).HasColumnName("intern_evaluation_id");
            entity.Property(e => e.Score).HasColumnName("score");

            entity.HasOne(d => d.FormCriteria).WithMany(p => p.InternEvaluationCriteria)
                .HasForeignKey(d => d.FormCriteriaId)
                .HasConstraintName("intern_evaluation_criteria_form_criteria_id_fkey");

            entity.HasOne(d => d.InternEvaluation).WithMany(p => p.InternEvaluationCriteria)
                .HasForeignKey(d => d.InternEvaluationId)
                .HasConstraintName("intern_evaluation_criteria_intern_evaluation_id_fkey");
                //.OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("role_id");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_date");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.UniversityId).HasName("university_pkey");

            entity.ToTable("university");

            entity.Property(e => e.UniversityId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("university_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_date");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(255)
                .HasColumnName("imgUrl");
            entity.Property(e => e.ModifyDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modify_date");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.UniversityName)
                .HasMaxLength(255)
                .HasColumnName("university_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("intern_pkey");

            entity.ToTable("user");

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CampaignId).HasColumnName("campaign_id");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_date");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('intern_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(255)
                .HasColumnName("imgUrl");
            entity.Property(e => e.MentorId).HasColumnName("mentor_id");
            entity.Property(e => e.ModifyDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modify_date");
            entity.Property(e => e.Otp).HasColumnName("otp");
            entity.Property(e => e.OtpExpired).HasColumnName("otp_expired");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .HasColumnName("telephone");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");

            entity.HasOne(d => d.Campaign).WithMany(p => p.Users)
                .HasForeignKey(d => d.CampaignId)
                .HasConstraintName("intern_campaign_id_fkey");

            entity.HasOne(d => d.Mentor).WithMany(p => p.InverseMentor)
                .HasForeignKey(d => d.MentorId)
                .HasConstraintName("mentor_id_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("role_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
