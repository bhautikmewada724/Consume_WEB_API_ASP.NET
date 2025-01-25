using FluentValidation;
using ApiConsume.Models;

namespace ApiConsume.Validation
{
    public class CityValidator : AbstractValidator<CityModel>
    {
        public CityValidator()
        {
            // Validate CityName
            RuleFor(city => city.CityName)
                .NotEmpty().WithMessage("City Name is required.")
                .MaximumLength(100).WithMessage("City Name cannot exceed 100 characters.");

            // Validate CountryID
            RuleFor(city => city.CountryID)
                .NotEmpty().WithMessage("Country ID is required.")
                .GreaterThan(0).WithMessage("Country ID must be greater than zero.");

            // Validate StateID
            RuleFor(city => city.StateID)
                .NotEmpty().WithMessage("State ID is required.")
                .GreaterThan(0).WithMessage("State ID must be greater than zero.");

            // Validate CityCode
            RuleFor(city => city.CityCode)
                .NotEmpty().WithMessage("City Code is required.")
                .MaximumLength(50).WithMessage("City Code cannot exceed 50 characters.");
        }
    }
}
