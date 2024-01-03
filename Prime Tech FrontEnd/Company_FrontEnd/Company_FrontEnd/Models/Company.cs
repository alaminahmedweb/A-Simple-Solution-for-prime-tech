using System.ComponentModel.DataAnnotations;

namespace Company_FrontEnd.Models
{
    public class Company
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string PhoneNumber { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public List<string> AdditionalColumns { get; set; } = new List<string>();
    }
}
