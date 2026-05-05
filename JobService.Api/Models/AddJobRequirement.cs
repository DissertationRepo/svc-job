namespace JobService.Api.Models
{
    public class AddJobRequirement
    {
        public string? JobId { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public bool IsMandatory { get; set; }
    }
}
