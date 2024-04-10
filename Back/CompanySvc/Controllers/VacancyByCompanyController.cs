using System.Security.Claims;
using CompanySvc.Repositories;
using Contracts;
using GlobalModels.Vacancy;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacancyService.Models;

namespace CompanySvc.Controllers;

[ApiController]
[Route("VacancyByCompany")]
public class VacancyByCompanyController: ControllerBase
{
    private readonly IPublishEndpoint _publisher;
    private readonly ICompanyRepo _companyRepo;

    public VacancyByCompanyController(IPublishEndpoint publisher, ICompanyRepo companyRepo)
    {
        _publisher = publisher;
        _companyRepo = companyRepo;
    }
    
    
    [Authorize]
    [HttpPost("PublishVacancy")]
    public async Task<IActionResult> PublishVacancy([FromBody] VacancyToAddDto vacancy)
    {
        Guid companyId = Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value);
        
        if (companyId == null || companyId == Guid.Empty)
            return Unauthorized();
        
        CompanyShortInfo? companyShortInfo = await _companyRepo.GetCompanyShortInfoById(companyId);
        
        if (companyShortInfo is null)
            throw new Exception("Company not found");

        AddVacancyEvent @event = new AddVacancyEvent(DateTime.UtcNow, companyShortInfo, vacancy);
        
        await _publisher.Publish(@event);
        
        return Ok();
    }


    // [Authorize]
    // [HttpPost("DeleteVacancy")]
    // public async Task<IActionResult> DeleteVacancy(Guid id)
    // {
    //     
    // }
}