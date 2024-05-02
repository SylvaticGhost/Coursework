namespace Contracts.Events.Messages;

public record CheckIfUserAppliedEvent(Guid UserId, Guid VacancyId)
{
    public DateTime Date { get; init; } = DateTime.UtcNow;
}