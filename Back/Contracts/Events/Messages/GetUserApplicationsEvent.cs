namespace Contracts.Events.ResponseOnVacancyEvents;

public record GetUserApplicationsEvent(
    Guid UserId
)
{
    public DateTime Date { get; init; } = DateTime.Now;
}