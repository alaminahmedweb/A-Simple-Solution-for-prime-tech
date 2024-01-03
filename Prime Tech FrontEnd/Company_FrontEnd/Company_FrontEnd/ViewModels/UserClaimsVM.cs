using System.Security.Claims;

namespace Company_FrontEnd.ViewModels
{
    public class UserClaimsVM
    {
        public UserClaimsVM()
        {
            Claims = new List<Claim>();
        }

        public string UserId { get; set; }
        public string ClaimType { get; set; }

        public List<Claim> Claims { get; set; }
    }

}
