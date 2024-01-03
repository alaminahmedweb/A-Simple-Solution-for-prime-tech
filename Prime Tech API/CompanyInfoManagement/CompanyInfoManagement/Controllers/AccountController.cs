using CompanyInfoManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompanyInfoManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Login([FromQuery] Login model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    return BadRequest(model);
                }
                var isValidUserAndPassword = await _userManager.CheckPasswordAsync(user, model.Password);
                if (isValidUserAndPassword == false)
                {
                    return BadRequest("Invalid Login Request..");
                }
                var result = await _userManager.FindByNameAsync(model.UserName);

                if (result != null)
                {
                    return Ok(result.Id);
                }
            }
            return BadRequest();

        }


    }
}
