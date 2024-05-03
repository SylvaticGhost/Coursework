namespace Contracts.Events.Messages;

public record DeleteUserApplicationByVacancyEvent(Guid UserId, Guid VacancyId)
{
    public DateTime Date { get; init; } = DateTime.Now;
}