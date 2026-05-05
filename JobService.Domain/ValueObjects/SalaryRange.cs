namespace JobService.Domain.ValueObjects;

public sealed record SalaryRange
{
    public decimal Min { get; }
    public decimal Max { get; }
    public string Currency { get; }

    public SalaryRange(decimal min, decimal max, string currency)
    {
        if (min < 0)
        {
            throw new ArgumentException("Minimum salary cannot be negative.", nameof(min));
        }

        if (max < min)
        {
            throw new ArgumentException("Maximum salary cannot be lower than minimum salary.", nameof(max));
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency is required.", nameof(currency));
        }

        var normalizedCurrency = currency.Trim().ToUpperInvariant();

        if (normalizedCurrency.Length != 3)
        {
            throw new ArgumentException("Currency must be a 3-letter ISO 4217 code.", nameof(currency));
        }

        Min = min;
        Max = max;
        Currency = normalizedCurrency;
    }

    public override string ToString() => $"{Min}-{Max} {Currency}";
}
