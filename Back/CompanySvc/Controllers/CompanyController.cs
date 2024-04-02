using CompanySvc.Models;
using CustomExceptions;
using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;

namespace CompanySvc.Controllers;

[ApiController]
[Route("Company")]
[DefaultExceptionFilter]
// Using a name CompanyController to avoid name collision with the Company model class
//and route name was simplified to belong to typical naming
public class CompanyController : ControllerBase
{
    private readonly ICompanyRepo _companyRepo;
    
    public CompanyController()
    {
        _companyRepo = new CompanyRepo();
    }
    
    
    [HttpGet("GetCompanyById")]
    public async Task<IActionResult> GetCompanyById(Guid id)
    {
        var company = await _companyRepo.GetCompanyById(id);
        if (company == null)
        {
            return NotFound();
        }

        return Ok(company);
    }
    
    
    [HttpGet("GetCompanyByName")]
    public async Task<IActionResult> GetCompanyByName(string name)
    {
        var company = await _companyRepo.GetCompanyByName(name);
        if (company == null)
            return NotFound();

        return Ok(company);
    }
    
    
    [HttpGet("GetCompanyShortInfoById")]
    public async Task<IActionResult> GetCompanyShortInfoById(Guid id)
    {
        var companyShortInfo = await _companyRepo.GetCompanyShortInfoById(id);
        if (companyShortInfo == null)
            return NotFound();

        return Ok(companyShortInfo);
    }
    
    
    //in future should be created a passing to admin controller to check
    [HttpPost("CreateCompany")]
    public async Task<IActionResult> CreateCompany(CompanyToAddDto companyToAddDto)
    {
        await _companyRepo.CreateCompany(companyToAddDto);
        return Ok();
    }
}