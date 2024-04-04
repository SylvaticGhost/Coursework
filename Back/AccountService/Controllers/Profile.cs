using System.Security.Claims;
using AccountService.Helpers;
using AccountService.Models;
using AccountService.Repositories.UserProfile;
using CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers;

[ApiController]
[Route("[controller]")]
[DefaultExceptionFilter]
public class Profile : ControllerBase
{
    private readonly IUserProfileRepository _userProfileRepository;
    public Profile()
    {
        _userProfileRepository = new UserProfileRepository();
    }

    
    
    [HttpGet("GetDefaultUserAvatar")]
    public Task<FileContentResult> GetDefaultUserAvatar()
    {
        return Task.FromResult(new FileContentResult(UserProfile.GetDefaultAvatar(), "image/png"));
    }
    
    
    
    [Authorize]
    [HttpPost("CreateUserProfile")]
    public async Task<IActionResult> CreateUserProfile([FromBody] UserProfileToAddDto userProfile)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
        
        if (!LocalValidator.ValidateProfile(userProfile))
            return new BadRequestObjectResult("Invalid input");
        
        await _userProfileRepository.CreateUserProfile(userProfile, userId);
        return new OkObjectResult(userId);
    }
    
}
