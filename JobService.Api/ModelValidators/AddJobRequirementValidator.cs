using JobService.Api.Models;
using FluentValidation;

namespace JobService.Api.ModelValidators
{
    public class AddJobRequirementValidator : AbstractValidator<AddJobRequirement>
    {
        public AddJobRequirementValidator()
        {
            RuleFor(x => x.JobId)
                .NotEmpty().WithMessage("JobId is required.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("JobId must be a valid GUID.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Category)
                .MaximumLength(80).WithMessage("Category cannot exceed 80 characters.");
        }
    }
}
