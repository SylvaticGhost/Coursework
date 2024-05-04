using System.Security.Claims;
using AccountService.Data;
using AccountService.Helpers;
using AccountService.Models;
using AccountService.Repositories;
using AccountService.Repositories.UserProfile;
using AutoMapper;
using CustomExceptions;
using CustomExceptions._400s;
using GlobalHelpers.Models;
using GlobalModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers;

[ApiController]
[Route("[controller]")]
[DefaultExceptionFilter]
public class Profile(DataContextNpgEf dataContextNpgEf, IMapper mapper) : ControllerBase
{
    private readonly IUserProfileRepository _userProfileRepository = new UserProfileRepository();
    private readonly IUserRepositoryBasic _userBasicInfoRepository = new UserProfileBasicInfoRepo(dataContextNpgEf);


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
    [HttpGet("GetOwnProfile")]
    public async Task<IActionResult> GetOwnProfile()
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
        if(userId == Guid.Empty)
            return new BadRequestObjectResult("Invalid user id");
        
        UserProfile? userProfile = await _userProfileRepository.GetUserProfile(userId);
        
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
            var nameAndSurname = await _userBasicInfoRepository.GetNameAndSurname(userId);
            
            if(nameAndSurname.FirstName == null || nameAndSurname.LastName == null)
                return new BadRequestObjectResult("User not found");
            
            await _userProfileRepository.CreateUserProfile(userProfile,nameAndSurname! , userId);
            return new OkObjectResult(userId);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }
    
    
    [Authorize]
    [HttpPost("UpdateUserProfile")]
    public async Task <IActionResult> UpdateUserProfile([FromBody] UserProfileToUpdateDto userProfile)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
        if(userId == Guid.Empty)
            return new BadRequestObjectResult("Invalid user id");
        
        LocalValidator localValidator = new(mapper);
        
        ValidationResults validationResult = localValidator.ValidateProfileToUpdate(userProfile);
        
        if(!validationResult.IsValid)
            return new BadRequestObjectResult(validationResult.ToString());

        try
        {
            await _userProfileRepository.UpdateUserProfile(userProfile, userId);
            return new OkResult();
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
        
        ValidationResults validationResult = LocalValidator.ValidateContact(contacts);

        if(!validationResult.IsValid)
            return new BadRequestObjectResult(validationResult.ToString());
        
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
