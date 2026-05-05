namespace JobService.Domain.Entities.ChildEntities;

using JobService.Domain.ValueObjects;

public sealed class JobRequirement
{
    public Guid Id { get; }
    public JobId JobId { get; }
    public string Description { get; private set; }
    public string? Category { get; private set; }
    public bool IsMandatory { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; private set; }

    private JobRequirement(
        Guid id,
        JobId jobId,
        string description,
        string? category,
        bool isMandatory,
        DateTime createdAt,
        DateTime updatedAt)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Requirement id cannot be empty.", nameof(id));

        JobId = jobId ?? throw new ArgumentNullException(nameof(jobId));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required.", nameof(description));

        Id = id;
        Description = description.Trim();
        Category = Normalize(category);
        IsMandatory = isMandatory;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static JobRequirement Create(JobId jobId, string description, string? category, bool isMandatory)
    {
        var now = DateTime.UtcNow;
        return new JobRequirement(Guid.NewGuid(), jobId, description, category, isMandatory, now, now);
    }

    public static JobRequirement Load(
        Guid id,
        JobId jobId,
        string description,
        string? category,
        bool isMandatory,
        DateTime createdAt,
        DateTime updatedAt)
    {
        return new JobRequirement(id, jobId, description, category, isMandatory, createdAt, updatedAt);
    }

    public void Update(string description, string? category, bool isMandatory)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required.", nameof(description));

        Description = description.Trim();
        Category = Normalize(category);
        IsMandatory = isMandatory;
        UpdatedAt = DateTime.UtcNow;
    }

    private static string? Normalize(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}
