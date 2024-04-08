using AutoMapper;
using CompanySvc.Helpers;
using CompanySvc.Models;
using CompanySvc.Repositories;
using CustomExceptions;
using VacancyService.Repositories;
using MassTransit;
using MassTransit.RabbitMqTransport;
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
    private readonly ICompanyRepoAuth _companyRepoAuth;
    private readonly IMapper _mapper;
    
    public CompanyController(IMapper mapper, IPublishEndpoint publisher)
    {
        _companyRepo = new CompanyRepo(publisher);
        _companyRepoAuth = new CompanyRepoAuth();
        _mapper = mapper;
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
        Company? company = await _companyRepo.GetCompanyByName(name);
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
        Guid companyId = await _companyRepo.CreateCompany(companyToAddDto);

        if (_companyRepoAuth.CheckIfCompanyHasAuth(companyId))
            return new BadRequestObjectResult("Company is registered");

        Guid key = await _companyRepoAuth.AddCompanyAuth(companyId);
        
        return Ok(new
        {
            CompanyId = companyId,
            Key = key
        });
    }


    [HttpPost("UpdateCompany")]
    public async Task<IActionResult> UpdateCompany(CompanyToUpdateDto company)
    {
        LocalValidator localValidator = new LocalValidator(_mapper);
        
        if (localValidator.ValidateCompanyForm(company) == false)
            return BadRequest();
        
        await _companyRepo.UpdateCompany(company);

        return Ok();
    }
    
    
    [HttpPost("DeleteCompany")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        if (!await _companyRepo.CheckIfCompanyExists(id))
            return BadRequest("Company with this id aren't exist");
        
        await _companyRepo.DeleteCompany(id);
        
        return Ok();
    }
}