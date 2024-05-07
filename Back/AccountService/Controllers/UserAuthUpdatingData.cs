using AccountService.ExceptionFilters;
using AccountService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CustomAttributes;

namespace AccountService.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
[LoggingExceptionFilter]
public class UserAuthUpdatingData(IUserRepository userRepository) : UserControllerBase
{
    [HttpPost("UpdatePassword")]
    public async Task<IActionResult> UpdatePassword(string newPassword)
    {
        Guid userId = GetUserId();
        
        if(userId == Guid.Empty)
            return new BadRequestObjectResult("Invalid user id");
        
        if(string.IsNullOrWhiteSpace(newPassword))
            return new BadRequestObjectResult("New password is required");

        await userRepository.UpdatePassword(newPassword, userId);

        return new OkResult();
    }


    [HttpPost("UpdateName")]
    public async Task<IActionResult> UpdateName(NameDto nameDto)
    {
        Guid userId = GetUserId();
        
        if(userId == Guid.Empty)
            return new BadRequestObjectResult("Invalid user id");
        
        if(string.IsNullOrWhiteSpace(nameDto.FirstName) && string.IsNullOrWhiteSpace(nameDto.LastName))
            return new BadRequestObjectResult("First name or last name are required");

        await userRepository.UpdateName(userId,nameDto.FirstName,nameDto.LastName);

        return new OkResult();
    }


    [CheckInputString]
    [HttpPost("UpdatePhoneNumber")]
    public async Task<IActionResult> UpdatePhone([FromQuery]string newPhoneNumber) {
        Guid userId = GetUserId();

        if(userId == Guid.Empty)
            return new BadRequestObjectResult("Invalid user id");

        await userRepository.UpdatePhoneNumber(newPhoneNumber, userId);

        return new OkResult();
    }
}