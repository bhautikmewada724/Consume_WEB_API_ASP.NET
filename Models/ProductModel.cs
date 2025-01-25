using System.ComponentModel.DataAnnotations;

namespace ApiConsume.Models
{
    public class ProductModel
    {
        public int? ProductID { get; set; }

        /*[Required(ErrorMessage = "Product name is required.")]*/
        public string ProductName { get; set; }

       /* [Required(ErrorMessage = "Product price is required.")]*/
        public double ProductPrice { get; set; }

        /*[Required(ErrorMessage = "Description is required.")]*/
        public string Description { get; set; }

        /*[Required(ErrorMessage = "Product code is required.")]*/
        public string ProductCode { get; set; }

      /*  [Required(ErrorMessage = "A valid User ID is required.")]*/
        public int UserID { get; set; }

        public string? UserName { get; set; }
    }
}
