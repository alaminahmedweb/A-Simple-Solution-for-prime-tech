using Company_FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Company_FrontEnd.Controllers
{
    public class CompaniesController : Controller
    {
        private HttpClient _httpClient;
        public CompaniesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            string apiUrl = $"http://localhost:5053/api/companies";
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            //await response.Content.ReadAsStringAsync();
            var newresponse=await response.Content.ReadAsStringAsync();

            List<Company> deserializeResponse = JsonConvert.DeserializeObject<List<Company>>(newresponse);

            return View(deserializeResponse);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string apiUrl = $"http://localhost:5053/api/companies/{id}";
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            //await response.Content.ReadAsStringAsync();
            var newresponse = await response.Content.ReadAsStringAsync();

            Company deserializeResponse = JsonConvert.DeserializeObject<Company>(newresponse);

            return View(deserializeResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Company company)
        {
            string apiUrl = $"http://localhost:5053/api/companies/";
            var response = await _httpClient.PutAsJsonAsync(apiUrl, new {id=company.Id,name=company.Name,address=company.Address});
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AddCompany(Company company)
        {
            string userId="";
            if (Request.Cookies["userId"] != null)
            {
                userId = Request.Cookies["userId"].ToString();
            }

            string apiUrl = $"http://localhost:5053/api/UserClaims?userId={userId}";
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var newresponse = await response.Content.ReadAsStringAsync();

            List<string> deserializeResponse = JsonConvert.DeserializeObject<List<string>>(newresponse);

            foreach(var item in deserializeResponse)
            {
                company.AdditionalColumns.Add(item);
            }
            return View(company);
        }

        [HttpPost]
        [ActionName("AddCompany")]
        public async Task<IActionResult> CreateCompany(Company company)
        {
            string apiUrl = $"http://localhost:5053/api/companies/";
            var response = await _httpClient.PostAsJsonAsync(apiUrl, new { name = company.Name, 
                address = company.Address,phoneNumber=company.PhoneNumber,postalCode=company.PostalCode });
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            string apiUrl = $"http://localhost:5053/api/companies/{id}";
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            var newresponse = await response.Content.ReadAsStringAsync();

            Company deserializeResponse = JsonConvert.DeserializeObject<Company>(newresponse);

            return View(deserializeResponse);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            string apiUrl = $"http://localhost:5053/api/companies/{id}";
            var response = await _httpClient.DeleteAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            var newresponse = await response.Content.ReadAsStringAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
