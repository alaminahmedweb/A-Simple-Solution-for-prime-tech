using CompanyInfoManagement.Models;

namespace CompanyInfoManagement.ViewModel
{
    public class UserClaimsVM
    {
        public UserClaimsVM() {
            Claims = new List<UserClaim>();
        }

        public string UserId { get; set; }
        public List<UserClaim> Claims { get; set; }
    }
}
