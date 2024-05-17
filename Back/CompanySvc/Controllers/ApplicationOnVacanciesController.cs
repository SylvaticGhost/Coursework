using System.Security.Claims;
using CompanySvc.MiddleWare.CompanyMessageController;
using CompanySvc.Repositories;
using Contracts;
using Contracts.Events.Messages;
using Contracts.Events.ResponseOnVacancyEvents;
using GlobalHelpers;
using GlobalHelpers.Models;
using GlobalModels.Messages;
using GlobalModels.Messages.CompanyResponse;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanySvc.Controllers;

[ApiController]
[Route("ApplicationOnVacancies")]
[Authorize]
public class ApplicationOnVacanciesController(
    ICompanyRepo companyRepo,
    ILogger<ApplicationOnVacanciesController> logger,
    IPublishEndpoint publisher,
    IConfiguration configuration,
    IRequestClient<GetVacancyApplicationEvent> requestGetResponsesOnVacancyClient,
    IRequestClient<PostAnswerOnApplicationEvent> requestFeedbackOnResponseClient)
    : CompanyControllerBase(publisher, configuration)
{
    [HttpGet("GetApplicationsOnVacancy")]
    public async Task<IActionResult> GetApplicationsOnVacancy(Guid vacancyId)
    {
        Guid companyId = GetCompanyId();
        
        GetVacancyApplicationEvent @event = new GetVacancyApplicationEvent(vacancyId, companyId);
        
        var response = await requestGetResponsesOnVacancyClient.GetResponse<IServiceBusResult<IEnumerable<UserApplicationOnVacancy>>>(@event);
        
        if (response.Message.IsSuccess)
            return Ok(response.Message.Result);
        
        logger.LogError(response.Message.ErrorMessage);
        return new BadRequestObjectResult("Error while getting applications on vacancy");
    }
    
    
    [ValidateAnswerOnApplication]
    [HttpPost("MakeFeedbackOnApplication")]
    public async Task<IActionResult> MakeFeedbackOnResponse([FromBody] AnswerOnApplicationToAddDto feedback)
    {
        Guid companyId = GetCompanyId();
        string companyName = await companyRepo.GetCompanyById(companyId).ContinueWith(c => c.Result!.Name);
        
        PostAnswerOnApplicationEvent @event = new PostAnswerOnApplicationEvent(feedback, companyId, companyName);
        
        var result = await requestFeedbackOnResponseClient.GetResponse<IServiceBusResult<bool>>(@event);
        
        if(result.Message.IsSuccess)
            return Ok(result.Message.Result);
        
        return new BadRequestObjectResult(result.Message.ErrorMessage);
    }
}