using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CompanyInfoManagement.Models
{
    public class Login
    {
        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}
