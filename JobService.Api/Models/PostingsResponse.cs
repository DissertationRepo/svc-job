namespace JobService.Api.Models
{
    public class PostingsResponse
    {
        public string? Id { get; init; }
        public string? JobId { get; init; }
        public string? Channel { get; init; }
        public string? Status { get; init; }
        public DateTime? PostedAt { get; init; }
        public DateTime? ExpiresAt { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
