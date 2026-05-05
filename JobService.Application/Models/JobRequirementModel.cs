namespace JobService.Application.Models
{
    public record JobRequirementModel
    {
        private readonly string _jobId;
        private readonly string _description;

        public JobRequirementModel(string jobId, string description, bool isMandatory)
        {
            _jobId = jobId ?? throw new ArgumentNullException(nameof(jobId), "JobId is required.");
            _description = !string.IsNullOrEmpty(description)
                ? description
                : throw new ArgumentNullException(nameof(description), "Description is required.");
            IsMandatory = isMandatory;
        }

        public string JobId => _jobId;
        public string Description => _description;
        public string? Category { get; init; }
        public bool IsMandatory { get; }
    }
}
