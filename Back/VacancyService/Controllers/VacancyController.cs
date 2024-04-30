using AutoMapper;
using CustomAttributes;
using GlobalModels.Vacancy;
using Microsoft.AspNetCore.Mvc;
using VacancyService.Models;
using VacancyService.Repositories;
using VacancyService.SearchContext;

namespace VacancyService.Controllers;

[ApiController]
[Route("Vacancy")]

public class VacancyController : ControllerBase
{
    private readonly IVacancyRepo _vacancyRepo;
    private readonly IMapper _mapper;
    
    public VacancyController(IMapper mapper)
    {
        _vacancyRepo = new VacancyRepo();
        _mapper = mapper;
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
    
    
    [HttpGet("GetLatestVacancies")]
    public async Task<IActionResult> GetLatestVacancies(uint count)
    {
        int c = (int)count;
        var vacancies = await _vacancyRepo.GetLatestVacancies(c);
        
        return Ok(vacancies);
    }
    
    
    [CheckHasNotNullParam<SearchVacancyParams>]
    [HttpPost("SearchVacancy")]
    public IActionResult SearchVacancy([FromBody] SearchVacancyParams searchVacancyParams)
    {
        ISearchVacancyContext searchVacancyContext = new SearchVacancyContext();
        
        var vacancies = searchVacancyContext.SearchVacancy(searchVacancyParams);
        
        return Ok(vacancies);
    }

    
    [CheckInputString]
    [HttpGet("SearchVacancyByKeyWords")]
    public IActionResult SearchVacancyByKeyWords(string keyWords)
    {
        ISearchVacancyContext searchVacancyContext = new SearchVacancyContext();
        
        List<VacancyDto> vacancies = new();
        
        SearchVacancyParams params1 = new()
        {
            Title = keyWords
        };
        
        var vacancies1 = searchVacancyContext.SearchVacancy(params1);
        vacancies.AddRange(_mapper.Map<List<VacancyDto>>(vacancies1));
        
        SearchVacancyParams params2 = new()
        {
            Specialization = keyWords
        };
        
        var vacancies2 = searchVacancyContext.SearchVacancy(params2);
        vacancies.AddRange(_mapper.Map<List<VacancyDto>>(vacancies2));
        
        SearchVacancyParams params3 = new()
        {
            CompanyName = keyWords
        };
        
        var vacancies3 = searchVacancyContext.SearchVacancy(params3);
        vacancies.AddRange(_mapper.Map<List<VacancyDto>>(vacancies3));
        
        return Ok(vacancies);
    }
}