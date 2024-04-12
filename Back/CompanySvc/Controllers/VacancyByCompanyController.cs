using System.Security.Claims;
using CompanySvc.Repositories;
using Contracts;
using CustomExceptions._400s;
using GlobalModels.Vacancy;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacancyService.Models;

namespace CompanySvc.Controllers;

[ApiController]
[Route("VacancyByCompany")]
public class VacancyByCompanyController(
    IPublishEndpoint publisher,
    ICompanyRepo companyRepo,
    IRequestClient<GetCompanyVacanciesEvent> requestGetVacancyClient,
    IRequestClient<DeleteVacancyEvent> requestDeleteVacancyClient)
    : ControllerBase
{
    [Authorize]
    [HttpPost("PublishVacancy")]
    public async Task<IActionResult> PublishVacancy([FromBody] VacancyToAddDto vacancy)
    {
        Guid companyId = Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
        
        if (companyId == Guid.Empty)
            return Unauthorized();
        
        CompanyShortInfo? companyShortInfo = await companyRepo.GetCompanyShortInfoById(companyId);
        
        if (companyShortInfo is null)
            throw new Exception("Company not found");

        AddVacancyEvent @event = new AddVacancyEvent(DateTime.UtcNow, companyShortInfo, vacancy);
        
        await publisher.Publish(@event);
        
        return Ok();
    }


    [Authorize]
    [HttpPost("DeleteVacancy")]
    public async Task<IActionResult> DeleteVacancy(Guid id)
    {
        Guid companyId = GetCompanyId();
        
        DeleteVacancyEvent @event = new DeleteVacancyEvent(VacancyId: id,CompanyId: companyId, DateTime.UtcNow);

        try
        {
            Console.WriteLine("Sending request to delete vacancy");
            
            var result = await requestDeleteVacancyClient.GetResponse<IServiceBusResult<bool>>(@event);
            if (result.Message.IsSuccess)
                return Ok(result.Message.Result);
            
            Console.WriteLine("Get an error in DeleteVacancy method from request result to delete vacancy consumer");
            return new BadRequestObjectResult(result.Message.ErrorMessage);
        }
        catch (ForbiddenException ex)
        {
            Console.WriteLine("Catched an error in DeleteVacancy method ForbiddenException");
            return new BadRequestObjectResult(ex.Message);
        }
        catch (RequestTimeoutException)
        {
            Console.WriteLine("Catched an error in DeleteVacancy method RequestTimeoutException");
            return new BadRequestObjectResult("Request timeout");
        }
    }


    [Authorize]
    [HttpGet("GetCompanyVacancies")]
    public async Task<IActionResult> GetCompanyVacancies()
    {
        Guid companyId = GetCompanyId();
        
        GetCompanyVacanciesEvent @event = new GetCompanyVacanciesEvent(companyId);
        
        try
        {
            Console.WriteLine("Sending request to get company vacancies");
            await publisher.Publish(@event);
            var result = await requestGetVacancyClient.GetResponse<IServiceBusResult<IEnumerable<Vacancy>>>(@event);
            if (result.Message.IsSuccess)
                return Ok(result.Message.Result);

            return new BadRequestObjectResult(result.Message.ErrorMessage);
        }
        catch (RequestTimeoutException)
        {
            return new BadRequestObjectResult("Request timeout");
        }
    }
    
    
    private Guid GetCompanyId() =>
        Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
}