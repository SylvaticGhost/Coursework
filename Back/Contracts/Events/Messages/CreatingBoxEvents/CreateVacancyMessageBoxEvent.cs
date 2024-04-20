namespace Contracts.Events.Messages.CreatingBoxEvents;

public record CreateVacancyMessageBoxEvent(Guid VacancyId, Guid CompanyId)
{
    public DateTime Time { get; init; } = DateTime.UtcNow;
}