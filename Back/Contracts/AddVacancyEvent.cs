using GlobalModels.Vacancy;
using VacancyService.Models;

namespace Contracts;

public record AddVacancyEvent(
    DateTime Time,
    CompanyShortInfo CompanyShortInfo,
    VacancyToAddDto Vacancy
    );