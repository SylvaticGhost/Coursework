using VacancyService.Models;

namespace DefaultNamespace;

public interface IVacancyRepo
{
    public Task<Vacancy?> GetVacancy(Guid id);
}