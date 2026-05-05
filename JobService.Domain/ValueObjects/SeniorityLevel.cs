namespace JobService.Domain.ValueObjects;

public sealed record SeniorityLevel
{
    private static readonly HashSet<string> Allowed = new(StringComparer.OrdinalIgnoreCase)
    {
        "Intern",
        "Junior",
        "Mid",
        "Senior",
        "Lead",
        "Principal"
    };

    public string Value { get; }

    public SeniorityLevel(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("SeniorityLevel is required.", nameof(value));
        }

        var normalized = value.Trim();

        if (!Allowed.Contains(normalized))
        {
            throw new ArgumentException(
                $"SeniorityLevel '{value}' is not supported. Allowed values: {string.Join(", ", Allowed)}.",
                nameof(value));
        }

        Value = normalized;
    }

    public override string ToString() => Value;
}
