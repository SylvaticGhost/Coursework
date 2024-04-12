using GlobalModels.Vacancy;

namespace Contracts.VacancyEvent;

public record UpdateVacancyEvent(
    Guid CompanyId,
    VacancyToUpdateDto VacancyToUpdate,
    DateTime Time);