using System.Security.Claims;
using Contracts;
using GlobalModels;
using GlobalModels.Messages;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AccountService.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class VacancyByUser(IRequestClient<ApplicationOnVacancyPostEvent> requestClient) : ControllerBase
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
    
    
    [HttpGet("GetMyResponses")]
    public async Task<IActionResult> GetMyResponsesEndPoint()
    {
        Guid userId = GetUserId();
        
        throw new NotImplementedException();
    }


    [HttpGet("GetVacancyByUser")]
    public async Task<IActionResult> GetVacancyByUserEndPoint()
    {
        Guid userId = GetUserId();
        
        throw new NotImplementedException();
    }
    
    
    private Guid GetUserId() => Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
}