using System.Security.Claims;
using AccountService.ExceptionFilters;
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
[LoggingExceptionFilter]
public class VacancyByUser(IRequestClient<ApplicationOnVacancyPostEvent> applicationPostClient,
    IRequestClient<DeleteUsersApplicationEvent> deleteApplicationClient, 
    IRequestClient<CheckIfUserAppliedEvent> checkIfUserAppliedClient,
    IRequestClient<GetUserApplicationsEvent> getUserApplicationsClient,
    IRequestClient<GetUserApplicationOnVacancyEvent> getUserApplicationOnVacancyClient,
    IRequestClient<DeleteUserApplicationByVacancyEvent> deleteApplicationByVacancyClient,
    ILogger<VacancyByUser> logger) : ControllerBase
{
    [HttpPost("ResponseOnVacancy")]
    public async Task<IActionResult> ResponseOnVacancyEndPoint([FromBody] UserApplicationOnVacancyToAddDto? applicationOnVacancy)
    {
        if (applicationOnVacancy is null)
            return BadRequest("Invalid request body");
        
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
        
        ApplicationOnVacancyPostEvent postEvent = new(applicationOnVacancy, userId);
        
        var responseOnVacancyEvent = await applicationPostClient.GetResponse<IServiceBusResult<bool>>(postEvent);

        if (responseOnVacancyEvent.Message.IsSuccess) 
            return Ok();
        
        logger.LogError(responseOnVacancyEvent.Message.ErrorMessage);
        return BadRequest(responseOnVacancyEvent.Message.ErrorMessage);
    }
    
    
    [HttpGet("CheckIfUserApplied")]
    public async Task<IActionResult> CheckIfUserAppliedEndPoint(Guid vacancyId)
    {
        Guid userId = GetUserId();
        
        CheckIfUserAppliedEvent checkIfUserAppliedEvent = new(userId, vacancyId);
        
        var response = await checkIfUserAppliedClient.GetResponse<IServiceBusResult<bool>>(checkIfUserAppliedEvent);
        
        if (!response.Message.IsSuccess)
            return BadRequest(response.Message.ErrorMessage);
        
        return Ok(response.Message.Result);
    }
    
    
    [HttpGet("GetMyApplications")]
    public async Task<IActionResult> GetMyApplicationsEndPoint()
    {
        Guid userId = GetUserId();
        
        GetUserApplicationsEvent getApplicationsEvent = new(userId);
        
        var response = await getUserApplicationsClient.GetResponse<IServiceBusResult<List<UserApplicationOnVacancy>>>(getApplicationsEvent);
        
        if (!response.Message.IsSuccess)
            return BadRequest(response.Message.ErrorMessage);
        
        if (response.Message.Result is null)
            return Ok(null);
        
        Guid[] vacancyIds = response.Message.Result.Select(x => x.VacancyId).ToArray();
        
        var vacanciesEvent = new GetVacanciesEvent(vacancyIds);
        
        var responseVacancies = await getUserApplicationsClient.GetResponse<IServiceBusResult<IEnumerable<VacancyDto>>>(vacanciesEvent);
        
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
    
    [HttpGet("GetMyApplicationOnVacancy")]
    public async Task<IActionResult> GetMyApplicationsOnVacancyEndPoint(Guid vacancyId)
    {
        Guid userId = GetUserId();
        
        GetUserApplicationOnVacancyEvent getApplicationsEvent = new(userId, vacancyId);
        
        var response = await getUserApplicationOnVacancyClient.GetResponse<IServiceBusResult<UserApplicationOnVacancy>>(getApplicationsEvent);
        
        if (!response.Message.IsSuccess)
            return BadRequest(response.Message.ErrorMessage);
        
        if (response.Message.Result is null)
            return Ok(null);
        
        var applications = response.Message.Result;
        
        return Ok(applications);
    }


    [HttpPost("DeleteApplication")]
    public async Task<IActionResult> DeleteApplicationEndPoint(Guid applicationId)
    {
        Guid userId = GetUserId();
        
        DeleteUsersApplicationEvent deleteUsersApplicationEvent = new(applicationId);
        
        var response = await deleteApplicationClient.GetResponse<IServiceBusResult<bool>>(deleteUsersApplicationEvent);
        
        if (!response.Message.IsSuccess)
            return BadRequest(response.Message.ErrorMessage);
        
        return Ok();
    }
    
    
    [HttpPost("DeleteApplicationByVacancyId")]
    public async Task<IActionResult> DeleteApplicationOnVacancyEndPoint(Guid vacancyId)
    {
        Guid userId = GetUserId();
        
        DeleteUserApplicationByVacancyEvent deleteUsersApplicationEvent = new(userId, vacancyId);
        
        var response = await deleteApplicationByVacancyClient.GetResponse<IServiceBusResult<bool>>(deleteUsersApplicationEvent);
        
        if (!response.Message.IsSuccess)
            return BadRequest(response.Message.ErrorMessage);
        
        return Ok();
    }
    
    
    private Guid GetUserId() => Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
}