using ApiConsume.Models;
using FluentValidation;



namespace ApiConsume.Validation
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            // Validate UserName
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("Please enter UserName");

            // Validate Email
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Please enter email")
                .EmailAddress().WithMessage("Invalid email format");

            // Validate Password
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("This field cannot be empty")
                .MinimumLength(3).WithMessage("Password should have a minimum of 3 characters")
                .MaximumLength(12).WithMessage("Password should have a maximum of 12 characters");

            // Validate Mobile Number
            RuleFor(user => user.MobileNo)
                .NotEmpty().WithMessage("This field cannot be empty")
                .Length(10).WithMessage("Mobile number should be exactly 10 digits")
                .Matches(@"^\d{10}$").WithMessage("Mobile number must contain only digits");

            // Validate Address
            RuleFor(user => user.Address)
                .NotEmpty().WithMessage("This field cannot be empty");

            // Validate IsActive
            RuleFor(user => user.IsActive)
                .NotNull().WithMessage("This field cannot be empty");
        }
    }
}

