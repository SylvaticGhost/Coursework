namespace Contracts.Events.Messages;

public abstract record Event
{
    public DateTime Date { get; init; } = DateTime.Now;
}