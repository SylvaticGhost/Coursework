using GlobalModels.Vacancy;
using VacancyService.Models;

namespace VacancyService.SearchContext;

public interface ISearchVacancyContext
{
    public IEnumerable<Vacancy> SearchVacancy(SearchVacancyParams parameters);
}