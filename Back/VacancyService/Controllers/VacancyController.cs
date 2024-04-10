using Contracts;
using CustomExceptions;
using GlobalModels.Vacancy;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using VacancyService.Models;
using VacancyService.Repositories;

namespace VacancyService.Controllers;

[ApiController]
[Route("Vacancy")]

public class VacancyController : ControllerBase
{
    private readonly IVacancyRepo _vacancyRepo;
    private readonly IPublishEndpoint _endpoint;
    private readonly IRequestClient<GetCompanyShortInfoEvent> _requestClient;
    
    public VacancyController(IPublishEndpoint endpoint, IRequestClient<GetCompanyShortInfoEvent> requestClient)
    {
        _vacancyRepo = new VacancyRepo();
        _endpoint = endpoint;
        _requestClient = requestClient;
    }
    
    
    [HttpGet("GetVacancy")]
    public async Task<IActionResult> GetVacancy(Guid id)
    {
        var vacancy = await _vacancyRepo.GetVacancy(id);
        
        if (vacancy == null)
        {
            return NotFound();
        }
        
        return Ok(vacancy);
    }
    
    
    
    [HttpPost("DeleteVacancy")]
    public async Task<IActionResult> DeleteVacancy(Guid id)
    {
        if (!await _vacancyRepo.CheckIfVacancyExists(id))
            return BadRequest("Vacancy with this id aren't exist");
        
        await _vacancyRepo.DeleteVacancy(id);
        
        return Ok();
    }
}