using FluentValidation;
using ApiConsume.Models;

namespace ApiConsume.Validation
{
    public class OrderDetailValidator : AbstractValidator<OrderDetailModel>
    {
        public OrderDetailValidator()
        {
            // Validate OrderID
            RuleFor(detail => detail.OrderID)
                .NotEmpty().WithMessage("Order ID is required.")
                .GreaterThan(0).WithMessage("Order ID must be greater than zero.");

            // Validate ProductID
            RuleFor(detail => detail.ProductID)
                .NotEmpty().WithMessage("Product ID is required.")
                .GreaterThan(0).WithMessage("Product ID must be greater than zero.");

            // Validate Quantity
            RuleFor(detail => detail.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            // Validate Amount
            RuleFor(detail => detail.Amount)
                .NotEmpty().WithMessage("Amount is required.")
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");

            // Validate TotalAmount
            RuleFor(detail => detail.TotalAmount)
                .NotEmpty().WithMessage("Total Amount is required.")
                .GreaterThan(0).WithMessage("Total Amount must be greater than zero.");

            // Validate UserID
            RuleFor(detail => detail.UserID)
                .NotEmpty().WithMessage("User ID is required.")
                .GreaterThan(0).WithMessage("User ID must be greater than zero.");
        }
    }

    public class ProductDropDownValidator : AbstractValidator<ProductDropDownModel>
    {
        public ProductDropDownValidator()
        {
            // Validate ProductID
            RuleFor(product => product.ProductID)
                .NotEmpty().WithMessage("Product ID is required.")
                .GreaterThan(0).WithMessage("Product ID must be greater than zero.");

            // Validate ProductName
            RuleFor(product => product.ProductName)
                .NotEmpty().WithMessage("Product Name is required.")
                .MaximumLength(100).WithMessage("Product Name cannot exceed 100 characters.");
        }
    }
}
