using GlobalModels.Vacancy;
using VacancyService.Models;

namespace VacancyService.Repositories;

public interface IVacancyRepo
{
    public Task<Vacancy?> GetVacancy(Guid id);

    public Task<Guid> AddVacancy(VacancyToAddDto vacancy, CompanyShortInfo companyInfo, DateTime time = default);

    public Task UpdateCompanyInfoInVacancies(CompanyShortInfo companyInfo);

    public Task<bool> CheckIfVacancyExists(Guid id);

    public Task DeleteVacancy(Guid id);

    public Task DeleteCompanyVacancies(Guid companyId);
}