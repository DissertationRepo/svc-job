namespace JobService.Domain.ValueObjects;

public sealed record Location
{
    public string Value { get; }

    public Location(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Location is required.", nameof(value));
        }

        Value = value.Trim();
    }

    public override string ToString() => Value;
}
