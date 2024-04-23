using System.Security.Claims;
using CompanySvc.MiddleWare.CompanyMessageController;
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
    IPublishEndpoint publisher,
    IConfiguration configuration,
    IRequestClient<GetVacancyResponsesEvent> requestGetResponsesOnVacancyClient,
    IRequestClient<PostAnswerOnApplicationEvent> requestFeedbackOnResponseClient)
    : CompanyControllerBase(publisher, configuration)
{
    [HttpGet("GetApplicationsOnVacancy")]
    public async Task<IActionResult> GetApplicationsOnVacancy(Guid vacancyId)
    {
        Guid companyId = GetCompanyId();
        
        GetVacancyResponsesEvent @event = new GetVacancyResponsesEvent(vacancyId, companyId);
        
        var response = await requestGetResponsesOnVacancyClient.GetResponse<IServiceBusResult<IEnumerable<UserApplicationOnVacancy>>>(@event);
        
        if (response.Message.IsSuccess)
            return Ok(response.Message.Result);
        
        return new BadRequestObjectResult(response.Message.ErrorMessage);
    }
    
    
    [ValidateAnswerOnApplication]
    [HttpPost("MakeFeedbackOnApplication")]
    public async Task<IActionResult> MakeFeedbackOnResponse([FromBody] AnswerOnApplicationToAddDto feedback)
    {
        Guid companyId = GetCompanyId();
        
        PostAnswerOnApplicationEvent @event = new PostAnswerOnApplicationEvent(feedback);
        
        var result = await requestFeedbackOnResponseClient.GetResponse<IServiceBusResult<bool>>(@event);
        
        if(result.Message.IsSuccess)
            return Ok(result.Message.Result);
        
        return new BadRequestObjectResult(result.Message.ErrorMessage);
    }
}