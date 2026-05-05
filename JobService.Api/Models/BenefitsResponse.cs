namespace JobService.Api.Models
{
    public class BenefitsResponse
    {
        public string? Id { get; init; }
        public string? JobId { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
