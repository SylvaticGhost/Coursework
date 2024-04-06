using System.Security.Claims;
using AccountService.Helpers;
using AccountService.Models;
using AccountService.Repositories.UserProfile;
using CustomExceptions;
using GlobalModels;
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


    [HttpGet("GetUserProfile")]
    public async Task<IActionResult> GetUserProfile(string id)
    {
        if(string.IsNullOrWhiteSpace(id))
            return new BadRequestObjectResult("Id is required");
        
        if(!Guid.TryParse(id, out Guid userId))
            return new BadRequestObjectResult("Invalid id");
        
        UserProfile? userProfile = await _userProfileRepository.GetUserProfile(userId);
        
        if(userProfile == null)
            return new NotFoundObjectResult("User not found");
        
        return new OkObjectResult(userProfile);
    }

    
    [Authorize]
    [HttpPost("CreateUserProfile")]
    public async Task<IActionResult> CreateUserProfile([FromBody] UserProfileToAddDto userProfile)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
        Console.WriteLine("userId for creation profile: " + userId);
        if(userId == Guid.Empty)
            return new BadRequestObjectResult("Invalid user id");
        
        if (!LocalValidator.ValidateProfile(userProfile))
            return new BadRequestObjectResult("Invalid input");

        try
        {
            await _userProfileRepository.CreateUserProfile(userProfile, userId);
            return new OkObjectResult(userId);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }
    
    
    [Authorize]
    [HttpPost("AddContact")]
    public async Task<IActionResult> AddContact([FromBody] Contact contacts)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
        if(userId == Guid.Empty)
            return new BadRequestObjectResult("Invalid user id");
        
        // if (!LocalValidator.ValidateContact(contact))
        //     throw new BadRequestException("Invalid input");

        try
        {
            await _userProfileRepository.AddContacts(userId, [contacts]);
        
            return new OkResult();
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }
    
}
