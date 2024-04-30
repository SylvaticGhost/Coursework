using System.Security.Claims;
using Contracts;
using Contracts.Events.Messages;
using Contracts.Events.ResponseOnVacancyEvents;
using Contracts.Events.VacancyEvent;
using GlobalModels.Messages;
using GlobalModels.Vacancy;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AccountService.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class VacancyByUser(IRequestClient<ApplicationOnVacancyPostEvent> requestClient,
    IRequestClient<DeleteUsersApplicationEvent> deleteApplicationClient) : ControllerBase
{
    [HttpPost("ResponseOnVacancy")]
    public async Task<IActionResult> ResponseOnVacancyEndPoint([FromBody] UserApplicationOnVacancyToAddDto applicationOnVacancy)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
        
        ApplicationOnVacancyPostEvent postEvent = new(applicationOnVacancy, userId);
        
        var responseOnVacancyEvent = await requestClient.GetResponse<IServiceBusResult<bool>>(postEvent);
        
        if (!responseOnVacancyEvent.Message.IsSuccess)
            return BadRequest(responseOnVacancyEvent.Message.ErrorMessage);
        
        return Ok();
    }
    
    
    [HttpGet("GetMyApplications")]
    public async Task<IActionResult> GetMyApplicationsEndPoint()
    {
        Guid userId = GetUserId();
        
        GetUserApplicationsEvent getApplicationsEvent = new(userId);
        
        var response = await requestClient.GetResponse<IServiceBusResult<List<UserApplicationOnVacancy>>>(getApplicationsEvent);
        
        if (!response.Message.IsSuccess)
            return BadRequest(response.Message.ErrorMessage);
        
        if (response.Message.Result is null)
            return Ok(null);
        
        Guid[] vacancyIds = response.Message.Result.Select(x => x.VacancyId).ToArray();
        
        var vacanciesEvent = new GetVacanciesEvent(vacancyIds);
        
        var responseVacancies = await requestClient.GetResponse<IServiceBusResult<IEnumerable<VacancyDto>>>(vacanciesEvent);
        
        if (!responseVacancies.Message.IsSuccess)
            return BadRequest(responseVacancies.Message.ErrorMessage);
        
        if (responseVacancies.Message.Result is null)
            return Ok(null);
        
        //TODO: add check if size of vacancies collection and applications is equal
        
        var applications = response.Message.Result;
        var vacancies = responseVacancies.Message.Result;
        
        var result = VacancyWithUserApplication.FromVacancyAndUserApplications(vacancies, applications);
        
        return Ok(result);
    }


    [HttpPost("DeleteApplication")]
    public async Task<IActionResult> DeleteApplicationEndPoint(Guid applicationId)
    {
        Guid userId = GetUserId();
        
        DeleteUsersApplicationEvent deleteUsersApplicationEvent = new(userId, applicationId);
        
        var response = await deleteApplicationClient.GetResponse<IServiceBusResult<bool>>(deleteUsersApplicationEvent);
        
        if (!response.Message.IsSuccess)
            return BadRequest(response.Message.ErrorMessage);
        
        return Ok();
    }
    
    
    private Guid GetUserId() => Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
}