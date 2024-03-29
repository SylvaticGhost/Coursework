using AccountService.Models;
using AccountService.Repositories.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controlers;

[ApiController]
[Route("[controller]")]
public class Profile
{
    private readonly IUserProfileRepository _userProfileRepository;
    public Profile()
    {
        _userProfileRepository = new UserProfileRepository();
    }


    [HttpGet("GetDefaultUserAvatar")]
    [AllowAnonymous]
    public Task<FileContentResult> GetDefaultUserAvatar()
    {
        return Task.FromResult(new FileContentResult(UserProfile.GetDefaultAvatar(), "image/png"));
    }
    
    
    
}