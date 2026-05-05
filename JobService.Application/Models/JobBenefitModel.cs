namespace JobService.Application.Models
{
    public record JobBenefitModel
    {
        private readonly string _jobId;
        private readonly string _name;

        public JobBenefitModel(string jobId, string name)
        {
            _jobId = jobId ?? throw new ArgumentNullException(nameof(jobId), "JobId is required.");
            _name = !string.IsNullOrEmpty(name)
                ? name
                : throw new ArgumentNullException(nameof(name), "Name is required.");
        }

        public string JobId => _jobId;
        public string Name => _name;
        public string? Description { get; init; }
    }
}
