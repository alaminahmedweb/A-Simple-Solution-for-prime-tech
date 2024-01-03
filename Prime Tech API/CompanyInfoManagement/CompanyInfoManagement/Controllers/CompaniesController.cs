using Application.Common.Interfaces;
using CompanyInfoManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyInfoManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            this._companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            var list= await _companyService.GetAllAsync();
        
            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            
            var result = await _companyService.GetByIdAsync(id);
            
            if (result==null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany(Company company)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            
            var result=await _companyService.AddEntity(company);

            if(result.Id==0)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetCompanyById", new { id = company.Id }, company);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompany([FromBody]Company company)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _companyService.UpdateEntity(company);

            if (result ==false)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            if(id==0)
                return BadRequest();
            var data=await _companyService.GetByIdAsync(id);
            if(data==null)
            {
                return BadRequest("Company Not Found");
            }
            var deleteResult= await _companyService.DeleteEntity(id);
            if (deleteResult!=true)
            {
                return BadRequest("Company can't be deleted");
            }
            return NoContent() ;
        }
    }
}
