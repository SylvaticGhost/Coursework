using GlobalModels.Vacancy;
using VacancyService.Models;

namespace VacancyService.Repositories;

public interface IVacancyRepo
{
    public Task<Vacancy?> GetVacancy(Guid id);
    
    public Task<Guid> GetOwnerOfVacancy(Guid vacancyId);

    public Task<Guid> AddVacancy(VacancyToAddDto vacancy, CompanyShortInfo companyInfo, DateTime time = default);

    public Task UpdateCompanyInfoInVacancies(CompanyShortInfo companyInfo);
    
    public Task UpdateVacancy(VacancyToUpdateDto vacancy, DateTime time = default);

    public Task<bool> CheckIfVacancyExists(Guid id);
    
    public Task<bool> CheckIfCompanyOwnVacancy(Guid companyId, Guid vacancyId);

    public Task DeleteVacancy(Guid id);

    public Task DeleteCompanyVacancies(Guid companyId);
    
    public Task<IEnumerable<Vacancy>> GetLatestVacancies(int count);
    
    public Task<IEnumerable<Vacancy>> GetCompanyVacancies(Guid companyId);
}