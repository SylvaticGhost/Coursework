using AccountService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controlers;

[ApiController]
[Route("[controller]")]
public class Profile
{
    public Profile()
    {
        
    }


    [HttpGet("GetDefaultUserAvatar")]
    [AllowAnonymous]
    public Task<FileContentResult> GetDefaultUserAvatar()
    {
        return Task.FromResult(new FileContentResult(UserProfile.GetDefaultAvatar(), "image/png"));
    }
}