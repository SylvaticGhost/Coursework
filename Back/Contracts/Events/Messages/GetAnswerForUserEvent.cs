namespace Contracts.Events.Messages;

public record GetAnswerForUserEvent(Guid UserId) : Event;