using Microsoft.AspNetCore.Mvc;

namespace CompanySvc.Controllers.Stuff;

[ApiController]
[Route("[controller]")]
public class StuffController : ControllerBase
{
    [HttpGet("TestConnection")]
    public IActionResult TestConnection()
    {
        return Ok("Connection is working");
    }
}