using GlobalModels.Messages.CompanyResponse;

namespace Contracts.Events.Messages;

public record PostAnswerOnApplicationEvent(AnswerOnApplicationToAddDto Answer, Guid CompanyId, string CompanyName) 
    : Event;