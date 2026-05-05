namespace JobService.Infrastructure.Entities
{
    public class JobBenefitEntity
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public JobEntity Job { get; set; } = null!;
    }
}
