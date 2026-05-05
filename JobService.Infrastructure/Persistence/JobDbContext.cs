using JobService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobService.Infrastructure.Persistence;

public sealed class JobDbContext : DbContext
{
    public JobDbContext(DbContextOptions<JobDbContext> options)
        : base(options)
    {
    }

    public DbSet<JobEntity> Jobs => Set<JobEntity>();
    public DbSet<JobRequirementEntity> JobRequirements => Set<JobRequirementEntity>();
    public DbSet<JobBenefitEntity> JobBenefits => Set<JobBenefitEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobDbContext).Assembly);
    }
}
