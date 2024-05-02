using GlobalModels;
using GlobalModels.Messages;

namespace Contracts;

public record ApplicationOnVacancyPostEvent
{
    public UserApplicationOnVacancyToAddDto ResponseOnVacancy { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public DateTime Date { get; init; } = DateTime.UtcNow;
    
    public ApplicationOnVacancyPostEvent(UserApplicationOnVacancyToAddDto? responseOnVacancy, Guid userId)
    {
        ResponseOnVacancy = responseOnVacancy ?? throw new ArgumentNullException(nameof(responseOnVacancy));
        
        if (userId == Guid.Empty || userId == null)
            throw new ArgumentNullException(nameof(userId));
        UserId = userId;
    }
}