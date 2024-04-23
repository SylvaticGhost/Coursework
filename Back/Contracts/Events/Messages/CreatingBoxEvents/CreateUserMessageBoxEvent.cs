namespace Contracts.Events.Messages.CreatingBoxEvents;

public record CreateUserMessageBoxEvent(Guid UserId)
{
    public DateTime Date { get; init; } = DateTime.UtcNow;
}