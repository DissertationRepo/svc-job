namespace JobService.Api.Models
{
    public class RequirementsResponse
    {
        public string? Id { get; init; }
        public string? JobId { get; init; }
        public string? Description { get; init; }
        public string? Category { get; init; }
        public bool IsMandatory { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
