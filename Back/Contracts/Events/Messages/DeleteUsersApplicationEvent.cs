namespace Contracts.Events.Messages;

public record DeleteUsersApplicationEvent(Guid ApplicationId)
{
   public DateTime Date { get; init; } = DateTime.Now;
}