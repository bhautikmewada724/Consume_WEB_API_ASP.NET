using System.ComponentModel.DataAnnotations;

namespace ApiConsume.Models
{
    public class OrderDetailModel
    {
        public int? OrderDetailID { get; set; }

        /*[Required(ErrorMessage = "Order ID is required.")]*/
        public int OrderID { get; set; }

       /* [Required(ErrorMessage = "Product ID is required.")]*/
        public int ProductID { get; set; }

       /* [Required(ErrorMessage = "Quantity is required.")]*/
        public int Quantity { get; set; }

        /*[Required(ErrorMessage = "Amount is required.")]*/
        public decimal Amount { get; set; }

        /*[Required(ErrorMessage = "Total Amount is required.")]*/
        public decimal TotalAmount { get; set; }

       /* [Required(ErrorMessage = "User ID is required.")]*/
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? ProductName { get; set; }
        public string? OrderDate { get; set; }
    }

    public class ProductDropDownModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
    }

   
}
