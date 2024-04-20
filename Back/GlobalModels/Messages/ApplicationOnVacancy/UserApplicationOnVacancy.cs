namespace GlobalModels.Messages;

public class UserApplicationOnVacancy
{
    public Guid UserApplicationId { get; init; }
    public Guid UserId { get; init; }
    public Guid VacancyId { get; init; }
    public ShortResume ShortResume { get; init; }
    public DateTime Date { get; init; } = DateTime.UtcNow;
}