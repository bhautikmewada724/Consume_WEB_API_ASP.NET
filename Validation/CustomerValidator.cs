using FluentValidation;
using ApiConsume.Models;

namespace ApiConsume.Validation
{
    public class CustomerValidator : AbstractValidator<CustomerModel>
    {
        public CustomerValidator()
        {
            // Validate CustomerName
            RuleFor(customer => customer.CustomerName)
                .NotEmpty().WithMessage("Please enter this field.")
                .MaximumLength(100).WithMessage("Customer Name cannot exceed 100 characters.");

            // Validate HomeAddress
            RuleFor(customer => customer.HomeAddress)
                .NotEmpty().WithMessage("Please enter this field.")
                .MaximumLength(200).WithMessage("Home Address cannot exceed 200 characters.");

            // Validate Email
            RuleFor(customer => customer.Email)
                .NotEmpty().WithMessage("Please enter this field.")
                .EmailAddress().WithMessage("Email format is not valid.");

            // Validate MobileNo
            RuleFor(customer => customer.MobileNo)
                .NotEmpty().WithMessage("Please enter this field.")
                .Length(10).WithMessage("Mobile number should be of 10 digits.")
                .Matches(@"^\d{10}$").WithMessage("Mobile number must contain only digits.");

            // Validate GSTNO
            RuleFor(customer => customer.GSTNO)
                .NotEmpty().WithMessage("Please enter this field.")
                .MaximumLength(15).WithMessage("GSTNO cannot exceed 15 characters.");

            // Validate CityName
            RuleFor(customer => customer.CityName)
                .NotEmpty().WithMessage("Please enter this field.")
                .MaximumLength(100).WithMessage("City Name cannot exceed 100 characters.");

            // Validate PinCode
            RuleFor(customer => customer.PinCode)
                .NotEmpty().WithMessage("Please enter this field.")
                .Matches(@"^\d{6}$").WithMessage("Pin Code must be exactly 6 digits.");

            // Validate NetAmount
            RuleFor(customer => customer.NetAmount)
                .NotEmpty().WithMessage("Please enter this field.")
                .GreaterThanOrEqualTo(0).WithMessage("Net Amount must be greater than or equal to zero.");

            // Validate UserID
            RuleFor(customer => customer.UserID)
                .NotEmpty().WithMessage("Please enter this field.")
                .GreaterThan(0).WithMessage("User ID must be greater than zero.");
        }
    }

    public class UserDropDownValidator : AbstractValidator<UserDropDownModel>
    {
        public UserDropDownValidator()
        {
            // Validate UserID
            RuleFor(user => user.UserID)
                .NotEmpty().WithMessage("User ID is required.")
                .GreaterThan(0).WithMessage("User ID must be greater than zero.");

            // Validate UserName
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("User Name is required.")
                .MaximumLength(100).WithMessage("User Name cannot exceed 100 characters.");
        }
    }
}
