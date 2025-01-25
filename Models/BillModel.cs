using System.ComponentModel.DataAnnotations;

namespace ApiConsume.Models
{
    public class BillModel
    {
        public int? BillID { get; set; }

        /*[Required(ErrorMessage = "Bill Number is required.")]*/
        public string BillNumber { get; set; }

        /*[Required(ErrorMessage = "Bill Date is required.")]*/
        public string BillDate { get; set; }

        /*[Required(ErrorMessage = "Order ID is required.")]*/
        public int OrderID { get; set; }

        /*[Required(ErrorMessage = "Total Amount is required.")]*/
        public decimal TotalAmount { get; set; }

       /* [Required(ErrorMessage = "Discount is required.")]*/
        public decimal Discount { get; set; }

        /*[Required(ErrorMessage = "Net Amount is required.")]*/
        public decimal NetAmount { get; set; }

        /*[Required(ErrorMessage = "User ID is required.")]*/
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? OrderDate{ get; set; }
    }

    public class OrderDropDownModel
    {
        public int OrderID { get; set; }
        public string OrderDate { get; set; }
    }
}
