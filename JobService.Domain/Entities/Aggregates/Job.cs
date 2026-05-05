using JobService.Domain.Entities.ChildEntities;
using JobService.Domain.ValueObjects;

namespace JobService.Domain.Entities.Aggregates;

public sealed class Job
{
    private readonly List<JobRequirement> _requirements = new();
    private readonly List<JobBenefit> _benefits = new();

    public JobId Id { get; }
    public string JobTitle { get; private set; }
    public string JobDescription { get; private set; }
    public SalaryRange SalaryRange { get; private set; }
    public Location Location { get; private set; }
    public EmploymentType EmploymentType { get; private set; }
    public RequiredSkill RequiredSkill { get; private set; }
    public SeniorityLevel SeniorityLevel { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; private set; }

    public IReadOnlyCollection<JobRequirement> Requirements => _requirements.AsReadOnly();
    public IReadOnlyCollection<JobBenefit> Benefits => _benefits.AsReadOnly();

    public Job(
        JobId id,
        string jobTitle,
        string jobDescription,
        SalaryRange salaryRange,
        Location location,
        EmploymentType employmentType,
        RequiredSkill requiredSkill,
        SeniorityLevel seniorityLevel)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));

        if (string.IsNullOrWhiteSpace(jobTitle))
            throw new ArgumentException("JobTitle is required.", nameof(jobTitle));

        if (string.IsNullOrWhiteSpace(jobDescription))
            throw new ArgumentException("JobDescription is required.", nameof(jobDescription));

        JobTitle = jobTitle.Trim();
        JobDescription = jobDescription.Trim();
        SalaryRange = salaryRange ?? throw new ArgumentNullException(nameof(salaryRange));
        Location = location ?? throw new ArgumentNullException(nameof(location));
        EmploymentType = employmentType ?? throw new ArgumentNullException(nameof(employmentType));
        RequiredSkill = requiredSkill ?? throw new ArgumentNullException(nameof(requiredSkill));
        SeniorityLevel = seniorityLevel ?? throw new ArgumentNullException(nameof(seniorityLevel));
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public static Job Create(
        string jobTitle,
        string jobDescription,
        decimal salaryMin,
        decimal salaryMax,
        string currency,
        string location,
        string employmentType,
        string requiredSkillName,
        string? requiredSkillLevel,
        string seniorityLevel)
    {
        return new Job(
            JobId.New(),
            jobTitle,
            jobDescription,
            new SalaryRange(salaryMin, salaryMax, currency),
            new Location(location),
            new EmploymentType(employmentType),
            new RequiredSkill(requiredSkillName, requiredSkillLevel),
            new SeniorityLevel(seniorityLevel));
    }

    public void UpdateDetails(
        string jobTitle,
        string jobDescription,
        SalaryRange salaryRange,
        Location location,
        EmploymentType employmentType,
        RequiredSkill requiredSkill,
        SeniorityLevel seniorityLevel)
    {
        if (string.IsNullOrWhiteSpace(jobTitle))
            throw new ArgumentException("JobTitle is required.", nameof(jobTitle));

        if (string.IsNullOrWhiteSpace(jobDescription))
            throw new ArgumentException("JobDescription is required.", nameof(jobDescription));

        JobTitle = jobTitle.Trim();
        JobDescription = jobDescription.Trim();
        SalaryRange = salaryRange ?? throw new ArgumentNullException(nameof(salaryRange));
        Location = location ?? throw new ArgumentNullException(nameof(location));
        EmploymentType = employmentType ?? throw new ArgumentNullException(nameof(employmentType));
        RequiredSkill = requiredSkill ?? throw new ArgumentNullException(nameof(requiredSkill));
        SeniorityLevel = seniorityLevel ?? throw new ArgumentNullException(nameof(seniorityLevel));
        UpdatedAt = DateTime.UtcNow;
    }
}
