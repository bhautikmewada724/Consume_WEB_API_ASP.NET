using FluentValidation;
using ApiConsume.Models;

namespace ApiConsume.Validation
{
    public class BillValidator : AbstractValidator<BillModel>
    {
        public BillValidator()
        {
            // Validate BillNumber
            RuleFor(bill => bill.BillNumber)
                .NotEmpty().WithMessage("Bill Number is required.")
                .MaximumLength(50).WithMessage("Bill Number cannot exceed 50 characters.");

            // Validate BillDate
            RuleFor(bill => bill.BillDate)
                .NotEmpty().WithMessage("Bill Date is required.");
               /* .Matches(@"^\d{4}-\d{2}-\d{2}$").WithMessage("Bill Date must be in the format YYYY-MM-DD.");*/

            // Validate OrderID
            RuleFor(bill => bill.OrderID)
                .NotEmpty().WithMessage("Order ID is required.")
                .GreaterThan(0).WithMessage("Order ID must be greater than zero.");

            // Validate TotalAmount
            RuleFor(bill => bill.TotalAmount)
                .NotEmpty().WithMessage("Total Amount is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Total Amount must be greater than or equal to zero.");

            // Validate Discount
            RuleFor(bill => bill.Discount)
                .NotEmpty().WithMessage("Discount is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Discount must be greater than or equal to zero.");

            // Validate NetAmount
            RuleFor(bill => bill.NetAmount)
                .NotEmpty().WithMessage("Net Amount is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Net Amount must be greater than or equal to zero.");

            // Validate UserID
            RuleFor(bill => bill.UserID)
                .NotEmpty().WithMessage("User ID is required.")
                .GreaterThan(0).WithMessage("User ID must be greater than zero.");
        }
    }

    public class OrderDropDownValidator : AbstractValidator<OrderDropDownModel>
    {
        public OrderDropDownValidator()
        {
            // Validate OrderID
            RuleFor(order => order.OrderID)
                .NotEmpty().WithMessage("Order ID is required.")
                .GreaterThan(0).WithMessage("Order ID must be greater than zero.");

            // Validate OrderDate
            RuleFor(order => order.OrderDate)
                .NotEmpty().WithMessage("Order Date is required.")
                .Matches(@"^\d{4}-\d{2}-\d{2}$").WithMessage("Order Date must be in the format YYYY-MM-DD.");
        }
    }
}
