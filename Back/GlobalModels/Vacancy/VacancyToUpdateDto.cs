namespace GlobalModels.Vacancy;

public record VacancyToUpdateDto(
    Guid VacancyId,
    string Title,
    string Description,
    string Salary,
    string Experience,
    string Specialization
    ) : IVacancyInputFields;