using FluentValidation;
using ApiConsume.Models;

namespace ApiConsume.Validation
{
    public class StateValidator : AbstractValidator<StateModel>
    {
        public StateValidator()
        {
            // Validate StateName
            RuleFor(state => state.StateName)
                .NotEmpty().WithMessage("State Name is required");

            // Validate CountryID
            RuleFor(state => state.CountryID)
                .NotEmpty().WithMessage("Country Name is required")
                .GreaterThan(0).WithMessage("Country ID must be greater than 0");

            // Validate StateCode
            RuleFor(state => state.StateCode)
                .NotEmpty().WithMessage("State Code is required");

            // Validate CountryName (if not null or empty)
            RuleFor(state => state.CountryName)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(100).WithMessage("Country Name cannot exceed 100 characters")
                .When(state => !string.IsNullOrEmpty(state.CountryName));
        }
    }
}
