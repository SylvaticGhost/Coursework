using GlobalModels;

namespace Contracts;

public record ResponseOnVacancyEvent
{
    public ResponseOnVacancy ResponseOnVacancy { get; init; }
    
    public DateTime Date { get; } = DateTime.UtcNow;
    
    public ResponseOnVacancyEvent(ResponseOnVacancy? responseOnVacancy)
    {
        ResponseOnVacancy = responseOnVacancy ?? throw new ArgumentNullException(nameof(responseOnVacancy));
    }
}