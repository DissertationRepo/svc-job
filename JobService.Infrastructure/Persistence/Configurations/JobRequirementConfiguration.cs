using JobService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobService.Infrastructure.Persistence.Configurations
{
    public sealed class JobRequirementConfiguration : IEntityTypeConfiguration<JobRequirementEntity>
    {
        public void Configure(EntityTypeBuilder<JobRequirementEntity> builder)
        {
            builder.ToTable("job_requirements");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.JobId)
                .HasColumnName("job_id")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.Category)
                .HasColumnName("category")
                .HasMaxLength(80);

            builder.Property(x => x.IsMandatory)
                .HasColumnName("is_mandatory")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired();
        }
    }
}
