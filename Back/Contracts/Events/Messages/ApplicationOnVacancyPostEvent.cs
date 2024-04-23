using GlobalModels;
using GlobalModels.Messages;

namespace Contracts;

public record ApplicationOnVacancyPostEvent
{
    public UserApplicationOnVacancyToAddDto ResponseOnVacancy { get;  }
    
    public Guid UserId { get; }
    
    public DateTime Date { get; } = DateTime.UtcNow;
    
    public ApplicationOnVacancyPostEvent(UserApplicationOnVacancyToAddDto? responseOnVacancy, Guid userId)
    {
        ResponseOnVacancy = responseOnVacancy ?? throw new ArgumentNullException(nameof(responseOnVacancy));
        UserId = userId;
    }
}