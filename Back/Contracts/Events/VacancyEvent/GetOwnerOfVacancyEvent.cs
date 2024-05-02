namespace Contracts.Events.VacancyEvent;

public record GetOwnerOfVacancyEvent(Guid VacancyId)
{
    public DateTime Date { get; init; } = DateTime.Now;
}