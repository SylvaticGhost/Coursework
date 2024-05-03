namespace Contracts.Events.Messages;

public record GetUserApplicationOnVacancyEvent(Guid UserId, Guid VacancyId) 
{
    public DateTime Date { get; init; } = DateTime.Now;
}