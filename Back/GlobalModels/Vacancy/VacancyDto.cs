namespace GlobalModels.Vacancy;

public record VacancyDto(
    Guid VacancyId,
    string Title,
    string Description,
    string Salary,
    string Experience,
    string Specialization,
    DateTime CreatedAt,
    DateTime UpdatedAt)
    : IVacancyInputFields;
