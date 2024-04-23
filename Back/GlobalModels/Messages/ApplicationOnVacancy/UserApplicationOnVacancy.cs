using System.ComponentModel.DataAnnotations;

namespace GlobalModels.Messages;

public class UserApplicationOnVacancy
{
    public Guid UserApplicationId { get; init; } = Guid.NewGuid();
    public Guid UserId { get; init; }
    public Guid VacancyId { get; init; }
    public required ShortResume ShortResume { get; init; }
    public DateTime Date { get; init; } = DateTime.UtcNow;
}