namespace JobService.Domain.ValueObjects;

public sealed record JobId
{
    public Guid Value { get; }

    public JobId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("JobId cannot be empty.", nameof(value));
        }

        Value = value;
    }

    public static JobId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();

    public static implicit operator Guid(JobId id) => id.Value;
}
