using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CompanyInfoManagement.Models
{
    public class User
    {
        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match..Please Check")]
        public string ConfirmPassword { get; set; }

    }
}
