using ApiConsume.Models;
using FluentValidation;

namespace ApiConsume.Validation
{
    public class CountryValidator:AbstractValidator<CountryModel>
    {
        public CountryValidator()
        {
            RuleFor(c=>c.CountryName).NotNull().WithMessage("Country name cannot be blank")
                .NotEmpty().WithMessage("Country name cannot be empty");

            RuleFor(c => c.CountryCode).NotNull().WithMessage("Country code cannot be blank")
                .NotEmpty().WithMessage("Country code cannot be empty");
        }
    }
}
