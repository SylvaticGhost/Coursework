namespace Contracts.Events.Messages;

public record DeleteAnswerEvent(Guid UserId,Guid ApplicationId) : Event;