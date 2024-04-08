using CompanySvc.Models;
using VacancyService.Models;

namespace VacancyService.Repositories;

public interface ICompanyRepo
{
    public Task<Company?> GetCompanyById(Guid id);
    
    public Task<Company?> GetCompanyByName(string name);

    public Task<CompanyShortInfo?> GetCompanyShortInfoById(Guid id);
    
    public Task<Guid> CreateCompany(CompanyToAddDto companyToAddDto);

    public Task UpdateCompany(CompanyToUpdateDto company);

    public Task DeleteCompany(Guid id);

    public Task<bool> CheckIfCompanyExists(Guid id);
}