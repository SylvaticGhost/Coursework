using MongoDB.Entities;
using VacancyService.Models;

namespace DefaultNamespace;

public class VacancyRepo: IVacancyRepo
{
    public VacancyRepo()
    {
        
    }
    
    
    public async Task<Guid> AddVacancy(VacancyToAddDto vacancy, CompanyShortInfo companyInfo)
    {
        Guid id = Guid.NewGuid();
        
        var vacancyToAdd = new Vacancy
        {
            Id = id,
            Title = vacancy.Title,
            Description = vacancy.Description,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            CompanyInfo = companyInfo
        };
        
        await vacancyToAdd.SaveAsync();
        
        return id;
    }
    
}