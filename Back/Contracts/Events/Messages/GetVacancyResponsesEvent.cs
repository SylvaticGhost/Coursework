namespace Contracts.Events.ResponseOnVacancyEvents;

public record GetVacancyResponsesEvent(Guid VacancyId, Guid CompanyId)
{
    public DateTime Date { get; } = DateTime.UtcNow;
}