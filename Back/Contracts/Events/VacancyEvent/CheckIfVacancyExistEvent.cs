namespace Contracts.Events.VacancyEvent;

public record CheckIfVacancyExistEvent(Guid VacancyId)
{
    public DateTime Date { get; init; } = DateTime.UtcNow;
}