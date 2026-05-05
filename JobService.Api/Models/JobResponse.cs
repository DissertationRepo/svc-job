namespace JobService.Api.Models
{
    public class JobResponse
    {
        public string? Id { get; init; }
        public string? JobTitle { get; init; }
        public string? JobDescription { get; init; }
        public decimal SalaryMin { get; init; }
        public decimal SalaryMax { get; init; }
        public string? Currency { get; init; }
        public string? Location { get; init; }
        public string? EmploymentType { get; init; }
        public string? RequiredSkillName { get; init; }
        public string? RequiredSkillLevel { get; init; }
        public string? SeniorityLevel { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
