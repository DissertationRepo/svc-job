using JobService.Api.Models;
using FluentValidation;

namespace JobService.Api.ModelValidators
{
    public class UpdateJobRequirementValidator : AbstractValidator<UpdateJobRequirement>
    {
        public UpdateJobRequirementValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("Id must be a valid GUID.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Category)
                .MaximumLength(80).WithMessage("Category cannot exceed 80 characters.");
        }
    }
}
