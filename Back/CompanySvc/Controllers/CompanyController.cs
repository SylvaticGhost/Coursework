using AutoMapper;
using CompanySvc.Helpers;
using CompanySvc.Models;
using CompanySvc.Repositories;
using CustomExceptions;
using MassTransit;
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
    private readonly LocalAuthHelpers _localAuthHelpers;
    
    public CompanyController(IMapper mapper, IPublishEndpoint publisher, IConfiguration configuration)
    {
        _companyRepo = new CompanyRepo(publisher);
        _companyRepoAuth = new CompanyRepoAuth();
        _mapper = mapper;
        _localAuthHelpers = new LocalAuthHelpers(configuration);
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
    public async Task<IActionResult> CreateCompany(CompanyToAddDto? companyToAddDto)
    {
        try
        {
            if(companyToAddDto == null)
                return BadRequest("Company data is empty");

            CompanyUniqueDataDto? c = _mapper.Map<CompanyToAddDto, CompanyUniqueDataDto>(companyToAddDto);
            if(c is null)
                return BadRequest("Company data is empty");
            if(await _companyRepo.CheckIfCompanyExists(c))
                return new BadRequestObjectResult("Company with this data already exists");
            
            Guid? companyId = await _companyRepo.CreateCompany(companyToAddDto);
            
            if(companyId is null || companyId == Guid.Empty)
                return new BadRequestObjectResult("Company wasn't created");
            
            if (_companyRepoAuth.CheckIfCompanyHasAuth(companyId ?? throw new ArgumentException("CompanyId is null")))
                return new BadRequestObjectResult("Company is registered");

            Guid? key = await _companyRepoAuth.AddCompanyAuth(companyId ?? throw new ArgumentException("CompanyId is null"));
            
            if(key == null || key == Guid.Empty)
                return new BadRequestObjectResult("Company wasn't registered");

            return Ok(new
            {
                CompanyId = companyId,
                Key = key
            });
        }
        catch(Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
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


    [HttpPost("CompanyHubLogin")]
    public async Task<IActionResult> CompanyHubLogin(CompanyToLoginDto? company)
    {
        Console.WriteLine("Executing CompanyHubLogin method in CompanyController");
        Console.WriteLine("Company id: " + company.CompanyId + ", key: " + company.Key);
        try
        {
            if(company is null)
                return BadRequest("Company data is empty");
            
            if(company.CompanyId == Guid.Empty || company.Key == Guid.Empty)
                return BadRequest("Company id or key is empty");
        
            bool result = await _companyRepoAuth.CompanyLogin(company);
        
            if (!result)
                return BadRequest("Company with this id and key aren't exist");
        
            string token = _localAuthHelpers.GenerateJwtToken(company.CompanyId);
        
            return Ok(token);
        }
        catch(Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }
}