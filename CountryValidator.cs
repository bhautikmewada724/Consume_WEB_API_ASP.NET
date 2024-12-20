using FluentValidation;
using WebAPI_Practice.Model;

namespace WebAPI_Practice
{
    public class CountryValidator : AbstractValidator<CountryModel>
    {
        public CountryValidator() {
            RuleFor(c => c.CountryName).NotEmpty();
            RuleFor(c => c.CountryCode).NotEmpty();
        }
    }
}
