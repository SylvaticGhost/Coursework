using System.Security.Claims;
using Contracts;
using GlobalModels;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AccountService.Controllers;

[ApiController]
[Route("[controller]")]
public class VacancyByUser(IRequestClient<ResponseOnVacancyEvent> requestClient) : ControllerBase
{
    [Authorize]
    [HttpPost("ResponseOnVacancy")]
    public async Task<IActionResult> ResponseOnVacancyEndPoint([FromBody] ResponseOnVacancyToAddDto responseOnVacancy)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
        
        ResponseOnVacancy response = new(userId, responseOnVacancy.VacancyId, responseOnVacancy.Resume, DateTime.Now);
        
        ResponseOnVacancyEvent @event = new(response);
        
        var responseOnVacancyEvent = await requestClient.GetResponse<IServiceBusResult<bool>>(@event);
        
        if (!responseOnVacancyEvent.Message.IsSuccess)
            return BadRequest(responseOnVacancyEvent.Message.ErrorMessage);
        
        return Ok();
    }
}