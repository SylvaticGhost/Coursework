using CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using VacancyService.Models;

namespace DefaultNamespace;

[ApiController]
[Route("Vacancy")]
[DefaultExceptionFilter]
public class VacancyController : ControllerBase
{
    private readonly IVacancyRepo _vacancyRepo;
    
    public VacancyController()
    {
        _vacancyRepo = new VacancyRepo();
    }
    
    [HttpGet("GetVacancy")]
    public async Task<IActionResult> GetVacancy(Guid id)
    {
        var vacancy = await _vacancyRepo.GetVacancy(id);
        
        if (vacancy == null)
        {
            return NotFound();
        }
        
        return Ok(vacancy);
    }
    
    
    [HttpPost("AddVacancy")]
    public async Task<IActionResult> AddVacancy(VacancyToAddDto vacancy)
    {
        return Problem();
    }
}