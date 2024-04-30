namespace Contracts.Events.VacancyEvent;

public record GetVacanciesEvent
{
    public DateTime Date { get; init; } = DateTime.UtcNow;
    
    public IEnumerable<Guid> VacancyIds { get; }
    
    public GetVacanciesEvent(IEnumerable<Guid> vacancyIds)
    {
        ArgumentNullException.ThrowIfNull(vacancyIds);

        var enumerable = vacancyIds as Guid[] ?? vacancyIds.ToArray();
        if(enumerable.Any(x => x == Guid.Empty))
            throw new ArgumentException("VacancyIds contains empty Guids", nameof(vacancyIds));

        VacancyIds = enumerable;
    }
}