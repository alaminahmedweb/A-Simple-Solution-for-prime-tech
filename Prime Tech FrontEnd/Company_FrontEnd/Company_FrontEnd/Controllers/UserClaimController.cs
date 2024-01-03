using Company_FrontEnd.Models;
using Company_FrontEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Company_FrontEnd.Controllers
{
    public class UserClaimController : Controller
    {

        private readonly HttpClient _httpClient;

        public UserClaimController(HttpClient httpClient) {
            this._httpClient = httpClient;
        }
        public IActionResult CreateClaim(UserClaimsVM model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("CreateClaim")]
        public async Task<IActionResult> CreateNewClaim(UserClaimsVM model)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim()
            {
                ClaimType = model.ClaimType,
                isActive=true
            });

            string apiUrl = $"http://localhost:5053/api/UserClaims/";
            var response = await _httpClient.PostAsJsonAsync(apiUrl, new { userId = model.UserId, claims = claims });
            response.EnsureSuccessStatusCode();
            var newresponse = await response.Content.ReadAsStringAsync();

            return RedirectToAction("Index", "User");

        }

    }
}
