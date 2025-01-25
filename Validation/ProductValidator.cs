using FluentValidation;
using ApiConsume.Models;

namespace ApiConsume.Validation
{
    public class ProductValidator : AbstractValidator<ProductModel>
    {
        public ProductValidator()
        {
            // Validate ProductName
            RuleFor(product => product.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters");

            // Validate ProductPrice
            RuleFor(product => product.ProductPrice)
                .NotEmpty().WithMessage("Product price is required.")
                .GreaterThan(0).WithMessage("Product price must be greater than zero");

            // Validate Description
            RuleFor(product => product.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            // Validate ProductCode
            RuleFor(product => product.ProductCode)
                .NotEmpty().WithMessage("Product code is required.")
                .Matches(@"^[A-Z0-9\-]+$").WithMessage("Product code must contain only uppercase letters, numbers, and hyphens");

            // Validate UserID
            RuleFor(product => product.UserID)
                .NotEmpty().WithMessage("A valid User ID is required.")
                .GreaterThan(0).WithMessage("User ID must be greater than zero");
        }
    }
}
