using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace ApiConsume.Models
{
    public class CustomerModel
    {
        //10
        public int? CustomerID { get; set; }


        /*[Required(ErrorMessage = "Please enter this feild")]*/
        public string CustomerName { get; set; }


        /*[Required(ErrorMessage = "Please enter this feild")]*/
        public string HomeAddress { get; set; }


        /*[Required(ErrorMessage = "Please enter this feild")]
        [EmailAddress(ErrorMessage ="email format not valid")]*/
        public string Email { get; set; }


        /*[Required(ErrorMessage = "Please enter this feild")]
        [StringLength(10, ErrorMessage = "mobile number should be of 10 digits")]*/

        public string MobileNo { get; set; }


        /*[Required(ErrorMessage = "Please enter this feild")]*/
        public string GSTNO { get; set; }


        /*[Required(ErrorMessage = "Please enter this feild")]*/
        public string CityName { get; set; }


        /*[Required(ErrorMessage = "Please enter this feild")]*/
        public string PinCode {  get; set; }


        /*[Required(ErrorMessage = "Please enter this feild")]*/
        public decimal NetAmount {  get; set; }


        /*[Required(ErrorMessage = "Please enter this feild")]*/
        public int UserID { get; set; }
        public string? UserName { get; set; }

    }

    public class UserDropDownModel 
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}
