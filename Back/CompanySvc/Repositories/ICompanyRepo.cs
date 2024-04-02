using CompanySvc.Models;
using VacancyService.Models;

namespace DefaultNamespace;

public interface ICompanyRepo
{
    public Task<Company?> GetCompanyById(Guid id);
    
    public Task<Company?> GetCompanyByName(string name);

    public Task<CompanyShortInfo?> GetCompanyShortInfoById(Guid id);
    public Task CreateCompany(CompanyToAddDto companyToAddDto);
}