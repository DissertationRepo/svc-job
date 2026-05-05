using JobService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobService.Infrastructure.Persistence.Configurations;

public sealed class JobConfiguration : IEntityTypeConfiguration<JobEntity>
{
    public void Configure(EntityTypeBuilder<JobEntity> builder)
    {
        builder.ToTable("jobs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.JobTitle)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("job_title");

        builder.Property(x => x.JobDescription)
            .IsRequired()
            .HasColumnType("text")
            .HasColumnName("job_description");

        builder.Property(x => x.SalaryMin)
            .IsRequired()
            .HasColumnType("numeric(18,2)")
            .HasColumnName("salary_min");

        builder.Property(x => x.SalaryMax)
            .IsRequired()
            .HasColumnType("numeric(18,2)")
            .HasColumnName("salary_max");

        builder.Property(x => x.Currency)
            .IsRequired()
            .HasMaxLength(3)
            .HasColumnName("currency");

        builder.Property(x => x.Location)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnName("location");

        builder.Property(x => x.EmploymentType)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnName("employment_type");

        builder.Property(x => x.RequiredSkillName)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("required_skill_name");

        builder.Property(x => x.RequiredSkillLevel)
            .HasMaxLength(50)
            .HasColumnName("required_skill_level");

        builder.Property(x => x.SeniorityLevel)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnName("seniority_level");

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.Property(x => x.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at");

        builder.HasMany(x => x.Requirements)
            .WithOne(x => x.Job)
            .HasForeignKey(x => x.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Benefits)
            .WithOne(x => x.Job)
            .HasForeignKey(x => x.JobId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
