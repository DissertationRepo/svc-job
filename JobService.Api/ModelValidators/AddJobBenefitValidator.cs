using JobService.Api.Models;
using FluentValidation;

namespace JobService.Api.ModelValidators
{
    public class AddJobBenefitValidator : AbstractValidator<AddJobBenefit>
    {
        public AddJobBenefitValidator()
        {
            RuleFor(x => x.JobId)
                .NotEmpty().WithMessage("JobId is required.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("JobId must be a valid GUID.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(120).WithMessage("Name cannot exceed 120 characters.");
        }
    }
}
