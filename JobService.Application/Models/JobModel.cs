namespace JobService.Application.Models
{
    public record JobModel
    {
        private readonly string _jobTitle;
        private readonly string _jobDescription;
        private readonly string _currency;
        private readonly string _location;
        private readonly string _employmentType;
        private readonly string _requiredSkillName;
        private readonly string _seniorityLevel;

        public JobModel(
            string jobTitle,
            string jobDescription,
            decimal salaryMin,
            decimal salaryMax,
            string currency,
            string location,
            string employmentType,
            string requiredSkillName,
            string seniorityLevel)
        {
            _jobTitle = !string.IsNullOrWhiteSpace(jobTitle)
                ? jobTitle
                : throw new ArgumentNullException(nameof(jobTitle), "JobTitle is required.");
            _jobDescription = !string.IsNullOrWhiteSpace(jobDescription)
                ? jobDescription
                : throw new ArgumentNullException(nameof(jobDescription), "JobDescription is required.");
            _currency = !string.IsNullOrWhiteSpace(currency)
                ? currency
                : throw new ArgumentNullException(nameof(currency), "Currency is required.");
            _location = !string.IsNullOrWhiteSpace(location)
                ? location
                : throw new ArgumentNullException(nameof(location), "Location is required.");
            _employmentType = !string.IsNullOrWhiteSpace(employmentType)
                ? employmentType
                : throw new ArgumentNullException(nameof(employmentType), "EmploymentType is required.");
            _requiredSkillName = !string.IsNullOrWhiteSpace(requiredSkillName)
                ? requiredSkillName
                : throw new ArgumentNullException(nameof(requiredSkillName), "RequiredSkill name is required.");
            _seniorityLevel = !string.IsNullOrWhiteSpace(seniorityLevel)
                ? seniorityLevel
                : throw new ArgumentNullException(nameof(seniorityLevel), "SeniorityLevel is required.");

            SalaryMin = salaryMin;
            SalaryMax = salaryMax;
        }

        public string JobTitle => _jobTitle;
        public string JobDescription => _jobDescription;
        public decimal SalaryMin { get; }
        public decimal SalaryMax { get; }
        public string Currency => _currency;
        public string Location => _location;
        public string EmploymentType => _employmentType;
        public string RequiredSkillName => _requiredSkillName;
        public string? RequiredSkillLevel { get; init; }
        public string SeniorityLevel => _seniorityLevel;
    }
}
