using System.ComponentModel.DataAnnotations;

namespace Company_FrontEnd.Models
{
    public class Login
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
