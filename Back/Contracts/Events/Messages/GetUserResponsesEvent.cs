namespace Contracts.Events.ResponseOnVacancyEvents;

public record GetUserResponsesEvent(
    Guid UserId
)
{
    public DateTime Date { get; init; } = DateTime.Now;
}