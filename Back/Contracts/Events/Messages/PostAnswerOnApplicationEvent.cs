using GlobalModels.Messages.CompanyResponse;

namespace Contracts.Events.Messages;

public record PostAnswerOnApplicationEvent(AnswerOnApplicationToAddDto AnswerOnApplicationToAddDto)
{
    public DateTime Date { get; init; } = DateTime.UtcNow;
}