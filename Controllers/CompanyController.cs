using Joboard.DTO.Company;
using Joboard.Service.Company;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Joboard.Controllers
{
    [Route("/api/Company")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllCompanys()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> GetCompanyById(int? id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            return Ok(company);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCompany(CompanyCreate_DTO companyCreate_DTO)
        {
            var result = await _companyService.CreateCompanyAsync(companyCreate_DTO);
            if (result)
            {
                return Ok(new { Message = "Company created successfully" });
            }
            else
            {
                return StatusCode(500, new { Message = "Failed to create Company" });
            }
        }


        [HttpPost("update/{CompanyId}")]
        public async Task<IActionResult> UpdateCompany(int? CompanyId, CompanyEdit_DTO companyEdit_DTO)
        {
            var result = await _companyService.UpdateCompanyAsync(CompanyId, companyEdit_DTO);
            if (result)
            {
                return Ok(new { Message = "Company updated successfully" });
            }
            else
            {
                return NotFound(new { Message = "Company not found" });
            }
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> DeleteCompany(int? Id)
        {
            var result = await _companyService.DeleteCompanyAsync(Id);
            if (result)
            {
                return Ok(new { Message = "Company deleted successfully" });
            }
            else
            {
                return NotFound(new { Message = "Company not found" });
            }
        }
    }
}
