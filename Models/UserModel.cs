using System.ComponentModel.DataAnnotations;

namespace ApiConsume.Models
{
    public class UserModel
    {
        public int? UserID { get; set; }

        /*[Required(ErrorMessage = "Please enter UserName")]*/
        public string UserName { get; set; }

        /*[Required(ErrorMessage = "Please enter email")]
        [EmailAddress(ErrorMessage = "Invalid email format")]*/
        public string Email { get; set; }

        /*[Required(ErrorMessage = "This field cannot be empty")]
        [MinLength(3, ErrorMessage = "Password should have a minimum of 3 characters")]
        [MaxLength(12, ErrorMessage = "Password should have a maximum of 12 characters")]*/
        public string Password { get; set; }

        /*[Required(ErrorMessage = "This field cannot be empty")]
        [StringLength(10, ErrorMessage = "Mobile number should be exactly 10 digits")]*/
        public string MobileNo { get; set; }

       /* [Required(ErrorMessage = "This field cannot be empty")]*/
        public string Address { get; set; }

        /*[Required(ErrorMessage = "This field cannot be empty")]*/
        public bool IsActive { get; set; }
    }

    public class UserLoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }

    public class UserRegisterModel
    {
        public int? UserID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
    }
}
