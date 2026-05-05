namespace JobService.Infrastructure.Entities
{
    public class JobRequirementEntity
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public string Description { get; set; } = null!;
        public string? Category { get; set; }
        public bool IsMandatory { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public JobEntity Job { get; set; } = null!;
    }
}
