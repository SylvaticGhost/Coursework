using Microsoft.AspNetCore.Mvc;
using VacancyService.Repositories;

namespace VacancyService.Controllers;

[ApiController]
[Route("VacancySupport")]
public class SupportControllers(IVacancyRepo vacancyRepo):ControllerBase
{
    [HttpGet("CheckIfCompanyOwnVacancy")]
    public async Task<IActionResult> CheckIfCompanyOwnVacancy(Guid companyId, Guid vacancyId)
    {
        bool result = await vacancyRepo.CheckIfCompanyOwnVacancy(companyId, vacancyId);
        return Ok(result);
    }
}