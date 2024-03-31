namespace VacancyService.Models;

public record VacancyToAddDto(
    string Title,
    string Description,
    Guid CompanyId
    );