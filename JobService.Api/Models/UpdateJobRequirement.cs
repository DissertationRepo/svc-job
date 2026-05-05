namespace JobService.Api.Models
{
    public class UpdateJobRequirement
    {
        public string? Id { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public bool IsMandatory { get; set; }
    }
}
