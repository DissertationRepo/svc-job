namespace JobService.Infrastructure.Entities;

public sealed class JobEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string JobTitle { get; set; } = null!;
    public string JobDescription { get; set; } = null!;
    public decimal SalaryMin { get; set; }
    public decimal SalaryMax { get; set; }
    public string Currency { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string EmploymentType { get; set; } = null!;
    public string RequiredSkillName { get; set; } = null!;
    public string? RequiredSkillLevel { get; set; }
    public string SeniorityLevel { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<JobRequirementEntity> Requirements { get; set; } = new List<JobRequirementEntity>();
    public ICollection<JobBenefitEntity> Benefits { get; set; } = new List<JobBenefitEntity>();
}
