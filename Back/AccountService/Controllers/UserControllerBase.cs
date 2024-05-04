using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers;

public class UserControllerBase : ControllerBase
{
    protected Guid GetUserId() => Guid.Parse(User.FindFirst(ClaimTypes.PrimarySid)?.Value!);
}