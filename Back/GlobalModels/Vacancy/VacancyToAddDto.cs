namespace GlobalModels.Vacancy;

public record VacancyToAddDto(
    string Title,
    string Description,
    string Salary,
    string Experience,
    string Specialization
    );