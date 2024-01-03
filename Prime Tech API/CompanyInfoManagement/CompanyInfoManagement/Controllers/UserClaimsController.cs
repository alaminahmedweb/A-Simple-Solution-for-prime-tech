using CompanyInfoManagement.Models;
using CompanyInfoManagement.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CompanyInfoManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserClaimsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserClaimsController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SaveUserClaims(UserClaimsVM model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return BadRequest(ModelState);
            }

            var claims=await _userManager.GetClaimsAsync(user);
            var result = await _userManager.AddClaimsAsync(user, 
                model.Claims.Select(c => new Claim(c.ClaimType, c.ClaimType)));
            if(!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetClaimsByUserId(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(ModelState);
            }

            var existingclaims = await _userManager.GetClaimsAsync(user);

            List<string> allClaims = new List<string>();

            foreach(var claim in existingclaims)
            {
                allClaims.Add(claim.Type);
            }

            return Ok(allClaims);
        }

    }
}
