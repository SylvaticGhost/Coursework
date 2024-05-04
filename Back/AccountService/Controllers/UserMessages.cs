using Contracts;
using Contracts.Events.Messages;
using GlobalModels.Messages.CompanyResponse;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserMessages(IRequestClient<GetAnswerForUserEvent> getAnswerForUserEventClient,
    IPublishEndpoint publishEndpoint,
    ILogger<UserMessages> logger) : UserControllerBase
{
    [HttpGet("GetMessages")]
    public async Task<IActionResult> GetUserMessages(Guid userId)
    {
        if (userId == Guid.Empty)
            return new BadRequestObjectResult("Invalid user id");
        
        GetAnswerForUserEvent getAnswerForUserEvent = new(userId);

        var response = await getAnswerForUserEventClient.GetResponse<IServiceBusResult<IEnumerable<AnswerOnApplication>>>(getAnswerForUserEvent);

        if (response.Message.IsSuccess)
            return Ok(response.Message.Result);
        
        logger.LogError(response.Message.ErrorMessage);
        return new BadRequestObjectResult("Internal server error");

    }
    
    
    public async Task<IActionResult> DeleteMessage(Guid applicationId)
    {
        if (applicationId == Guid.Empty)
            return new BadRequestObjectResult("Invalid answer id");

        Guid userId = GetUserId();

        DeleteAnswerEvent deleteAnswerEvent = new(userId, applicationId);
        
        await publishEndpoint.Publish(deleteAnswerEvent);
        
        return Ok();
    }
}