using FluentValidation;
using ApiConsume.Models;

namespace ApiConsume.Validation
{
    public class OrderValidator : AbstractValidator<OrderModel>
    {
        public OrderValidator()
        {
            // Validate CustomerID
            RuleFor(order => order.CustomerID)
                .NotEmpty().WithMessage("Customer ID is required.")
                .GreaterThan(0).WithMessage("Customer ID must be greater than zero.");

            // Validate OrderDate
            RuleFor(order => order.OrderDate)
                .NotEmpty().WithMessage("Order Date is required.")
                .Matches(@"^\d{4}-\d{2}-\d{2}$")
                .WithMessage("Order Date must be in the format YYYY-MM-DD.");

            // Validate PaymentMode
            RuleFor(order => order.PaymentMode)
                .NotEmpty().WithMessage("Payment Mode is required.")
                .MaximumLength(50).WithMessage("Payment Mode cannot exceed 50 characters.");

            // Validate ShippingAddress
            RuleFor(order => order.ShippingAddress)
                .NotEmpty().WithMessage("Shipping Address is required.")
                .MaximumLength(200).WithMessage("Shipping Address cannot exceed 200 characters.");

            // Validate TotalAmount
            RuleFor(order => order.TotalAmount)
                .NotEmpty().WithMessage("Total Amount is required.")
                .GreaterThan(0).WithMessage("Total Amount must be greater than zero.");

            // Validate UserID
            RuleFor(order => order.UserID)
                .NotEmpty().WithMessage("User ID is required.")
                .GreaterThan(0).WithMessage("User ID must be greater than zero.");
        }
    }

    public class CustomerDropDownValidator : AbstractValidator<CustomerDropDownModel>
    {
        public CustomerDropDownValidator()
        {
            // Validate CustomerID
            RuleFor(customer => customer.CustomerID)
                .NotEmpty().WithMessage("Customer ID is required.")
                .GreaterThan(0).WithMessage("Customer ID must be greater than zero.");

            // Validate CustomerName
            RuleFor(customer => customer.CustomerName)
                .NotEmpty().WithMessage("Customer Name is required.")
                .MaximumLength(100).WithMessage("Customer Name cannot exceed 100 characters.");
        }
    }
}
