using System.ComponentModel.DataAnnotations;

namespace ApiConsume.Models
{
    public class OrderModel
    {
        public int? OrderID { get; set; }

        /*[Required(ErrorMessage = "Customer ID is required.")]*/
        public int CustomerID { get; set; }

        /*[Required(ErrorMessage = "Order Date is required.")]*/
        public string OrderDate { get; set; }

       /* [Required(ErrorMessage = "Payment Mode is required.")]*/
        public string PaymentMode { get; set; }

       /* [Required(ErrorMessage = "Shipping Address is required.")]*/
        public string ShippingAddress { get; set; }

        /*[Required(ErrorMessage = "Total Amount is required.")]*/
        public decimal TotalAmount { get; set; }

        /*[Required(ErrorMessage = "User ID is required.")]*/
        public int UserID { get; set; }
        public string? CustomerName { get; set; }
        public string? UserName { get; set; }

    }

    public class CustomerDropDownModel
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
    }

    
}
