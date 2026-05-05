using JobService.Api.Models;
using FluentValidation;

namespace JobService.Api.ModelValidators
{
    public class UpdateJobBenefitValidator : AbstractValidator<UpdateJobBenefit>
    {
        public UpdateJobBenefitValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("Id must be a valid GUID.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(120).WithMessage("Name cannot exceed 120 characters.");
        }
    }
}
