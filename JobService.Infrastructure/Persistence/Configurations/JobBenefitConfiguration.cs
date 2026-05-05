using JobService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobService.Infrastructure.Persistence.Configurations
{
    public sealed class JobBenefitConfiguration : IEntityTypeConfiguration<JobBenefitEntity>
    {
        public void Configure(EntityTypeBuilder<JobBenefitEntity> builder)
        {
            builder.ToTable("job_benefits");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.JobId)
                .HasColumnName("job_id")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasColumnType("text");

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired();
        }
    }
}
