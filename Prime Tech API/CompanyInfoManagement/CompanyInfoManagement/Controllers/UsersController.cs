using CompanyInfoManagement.Models;
using CompanyInfoManagement.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompanyInfoManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager) 
        {
            this._userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            if (ModelState.IsValid)
            {
                //var user = new ApplicationUser { UserName = UserName, };
                var result =  _userManager.Users.ToList();
                if (result!=null)
                {
                    List<UsersVM> users = new List<UsersVM>();
                    foreach(var item in result)
                    {
                        UsersVM user = new UsersVM();
                        user.Id = item.Id;
                        user.Name = item.UserName;
                        users.Add(user);
                    }
                    return Ok(users);
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }


        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }


    }
}
