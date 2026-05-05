using JobService.Api.Models;
using FluentValidation;

namespace JobService.Api.ModelValidators
{
    public class NewJobValidator : AbstractValidator<NewJob>
    {
        public NewJobValidator()
        {
            RuleFor(x => x.JobTitle)
                .NotEmpty().WithMessage("JobTitle is required.")
                .MaximumLength(200).WithMessage("JobTitle cannot exceed 200 characters.");
            RuleFor(x => x.JobDescription)
                .NotEmpty().WithMessage("JobDescription is required.");
            RuleFor(x => x.SalaryMin)
                .GreaterThanOrEqualTo(0).WithMessage("SalaryMin must be greater than or equal to 0.");
            RuleFor(x => x.SalaryMax)
                .GreaterThanOrEqualTo(x => x.SalaryMin)
                .WithMessage("SalaryMax must be greater than or equal to SalaryMin.");
            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Length(3).WithMessage("Currency must be a 3-letter ISO 4217 code.");
            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required.");
            RuleFor(x => x.EmploymentType)
                .NotEmpty().WithMessage("EmploymentType is required.");
            RuleFor(x => x.RequiredSkillName)
                .NotEmpty().WithMessage("RequiredSkillName is required.")
                .MaximumLength(100).WithMessage("RequiredSkillName cannot exceed 100 characters.");
            RuleFor(x => x.SeniorityLevel)
                .NotEmpty().WithMessage("SeniorityLevel is required.");
        }
    }
}
