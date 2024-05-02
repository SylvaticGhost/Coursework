using System.ComponentModel.DataAnnotations;

namespace GlobalModels.Messages;

public class UserApplicationOnVacancy
{
    public Guid UserApplicationId { get; init; } = Guid.NewGuid();
    public Guid UserId { get; init; }
    public Guid VacancyId { get; init; }
    public ShortResume ShortResume { get; init; }
    public DateTime Date { get; init; } = DateTime.UtcNow;
    
    public UserApplicationOnVacancy(Guid userId, Guid vacancyId, ShortResume? shortResume)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User id is empty", nameof(userId));
        
        if (vacancyId == Guid.Empty)
            throw new ArgumentException("Vacancy id is empty", nameof(vacancyId));

        ArgumentNullException.ThrowIfNull(shortResume);

        UserId = userId;
        VacancyId = vacancyId;
        ShortResume = shortResume;
    }
}