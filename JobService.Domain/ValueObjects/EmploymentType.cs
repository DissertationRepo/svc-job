namespace JobService.Domain.ValueObjects;

public sealed record EmploymentType
{
    private static readonly HashSet<string> Allowed = new(StringComparer.OrdinalIgnoreCase)
    {
        "Full-Time",
        "Part-Time",
        "Contract",
        "Internship",
        "Temporary",
        "Freelance"
    };

    public string Value { get; }

    public EmploymentType(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("EmploymentType is required.", nameof(value));
        }

        var normalized = value.Trim();

        if (!Allowed.Contains(normalized))
        {
            throw new ArgumentException(
                $"EmploymentType '{value}' is not supported. Allowed values: {string.Join(", ", Allowed)}.",
                nameof(value));
        }

        Value = normalized;
    }

    public override string ToString() => Value;
}
