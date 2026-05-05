namespace JobService.Application.Models
{
    public record UpdateJobRequirementModel
    {
        private readonly string _id;
        private readonly string _description;

        public UpdateJobRequirementModel(string id, string description, bool isMandatory)
        {
            _id = id ?? throw new ArgumentNullException(nameof(id), "Id is required.");
            _description = !string.IsNullOrEmpty(description)
                ? description
                : throw new ArgumentNullException(nameof(description), "Description is required.");
            IsMandatory = isMandatory;
        }

        public string Id => _id;
        public string Description => _description;
        public string? Category { get; init; }
        public bool IsMandatory { get; }
    }
}
