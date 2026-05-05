namespace JobService.Application.Models
{
    public record UpdateJobBenefitModel
    {
        private readonly string _id;
        private readonly string _name;

        public UpdateJobBenefitModel(string id, string name)
        {
            _id = id ?? throw new ArgumentNullException(nameof(id), "Id is required.");
            _name = !string.IsNullOrEmpty(name)
                ? name
                : throw new ArgumentNullException(nameof(name), "Name is required.");
        }

        public string Id => _id;
        public string Name => _name;
        public string? Description { get; init; }
    }
}
