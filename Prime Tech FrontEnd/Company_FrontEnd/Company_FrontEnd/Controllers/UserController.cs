using Company_FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore;
using Newtonsoft.Json;
using Company_FrontEnd.ViewModels;

namespace Company_FrontEnd.Controllers
{
    public class UserController : Controller
    {
        private HttpClient _httpClient;
        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            string apiUrl = $"http://localhost:5053/api/Users";
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            //await response.Content.ReadAsStringAsync();
            var newresponse = await response.Content.ReadAsStringAsync();

            List<UsersVM> deserializeResponse = JsonConvert.DeserializeObject<List<UsersVM>>(newresponse);

            return View(deserializeResponse);
        }


        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> CreateUser(User user)
        {
            string apiUrl = $"http://localhost:5053/api/Users/";
            var response = await _httpClient.PostAsJsonAsync(apiUrl, new { userName = user.UserName, password = user.Password, confirmPassword=user.ConfirmPassword });
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login user)
        {
            string apiUrl = $"http://localhost:5053/api/Account?userName={user.UserName}&password={user.Password}";
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            var newresponse=await response.Content.ReadAsStringAsync();

            HttpContext.Response.Cookies.Append("userId", newresponse, new Microsoft.AspNetCore.Http.CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            });
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("userId");
            return RedirectToAction("Login", "User");
        }

    }
}
