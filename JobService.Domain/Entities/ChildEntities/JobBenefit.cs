namespace JobService.Domain.Entities.ChildEntities;

using JobService.Domain.ValueObjects;

public sealed class JobBenefit
{
    public Guid Id { get; }
    public JobId JobId { get; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; private set; }

    private JobBenefit(
        Guid id,
        JobId jobId,
        string name,
        string? description,
        DateTime createdAt,
        DateTime updatedAt)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Benefit id cannot be empty.", nameof(id));

        JobId = jobId ?? throw new ArgumentNullException(nameof(jobId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Benefit name is required.", nameof(name));

        Id = id;
        Name = name.Trim();
        Description = Normalize(description);
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static JobBenefit Create(JobId jobId, string name, string? description)
    {
        var now = DateTime.UtcNow;
        return new JobBenefit(Guid.NewGuid(), jobId, name, description, now, now);
    }

    public static JobBenefit Load(
        Guid id,
        JobId jobId,
        string name,
        string? description,
        DateTime createdAt,
        DateTime updatedAt)
    {
        return new JobBenefit(id, jobId, name, description, createdAt, updatedAt);
    }

    public void Update(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Benefit name is required.", nameof(name));

        Name = name.Trim();
        Description = Normalize(description);
        UpdatedAt = DateTime.UtcNow;
    }

    private static string? Normalize(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}
