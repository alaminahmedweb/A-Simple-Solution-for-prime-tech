using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CompanyInfoManagement.Models
{
    public class Company
    {
        public int Id { get; set; }
        
        [Unicode(false)]
        [MaxLength(250)]
        public string Name { get; set;}
        
        [Unicode(false)]
        [MaxLength(250)] 
        public string Address { get; set;}

        [Unicode(false)]
        [MaxLength(250)]
        public string PhoneNumber { get; set; }=string.Empty;

        [Unicode(false)]
        [MaxLength(250)]
        public string PostalCode { get; set; } = string.Empty;


    }
}
