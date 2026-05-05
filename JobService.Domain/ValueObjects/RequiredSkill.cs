namespace JobService.Domain.ValueObjects;

public sealed record RequiredSkill
{
    public string Name { get; }
    public string? Level { get; }

    public RequiredSkill(string name, string? level)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Required skill name is required.", nameof(name));
        }

        Name = name.Trim();
        Level = string.IsNullOrWhiteSpace(level) ? null : level.Trim();
    }

    public override string ToString() => Level is null ? Name : $"{Name} ({Level})";
}
