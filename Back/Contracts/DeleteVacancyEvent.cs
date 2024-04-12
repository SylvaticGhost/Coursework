namespace Contracts;

public record DeleteVacancyEvent(Guid VacancyId, Guid CompanyId, DateTime Time);