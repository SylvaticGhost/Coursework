namespace VacancyService.Models;

public record VacancyToAddDto(
    string Title,
    string Description,
    decimal Salary,
    Guid CompanyId
    );