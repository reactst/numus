using Microsoft.AspNetCore.Mvc;
using Numus.Shared.Dtoes.Company;
using Numus.Shared.Interfaces;
using System.Text.Json;

namespace Numus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyDto>>> GetAllCompanies()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetCompanyById(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(JsonSerializer.Serialize(company));
        }

        [HttpGet("externalId/{externalId}")]
        public async Task<ActionResult<CompanyDto>> GetCompanyByExternalId(Guid externalId)
        {
            var company = await _companyService.GetCompanyByExternalIdAsync(externalId);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpPost]
        public async Task<ActionResult<CompanyDto>> CreateCompany(CompanyDto companyDto)
        {
            try
            {
                await _companyService.CreateCompanyAsync(companyDto);
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{externalId}")]
        public async Task<ActionResult<CompanyDto>> UpdateCompany(Guid externalId, CompanyDto companyDto)
        {
            var updatedCompany = await _companyService.UpdateCompanyAsync(externalId, companyDto);
            if (updatedCompany == null)
            {
                return NotFound();
            }
            return Ok(updatedCompany);
        }

        [HttpDelete("{externalId}")]
        public async Task<ActionResult> DeleteCompany(Guid externalId)
        {
            var result = await _companyService.DeleteCompanyAsync(externalId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
