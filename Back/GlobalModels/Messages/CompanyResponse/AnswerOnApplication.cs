namespace GlobalModels.Messages.CompanyResponse;

public record AnswerOnApplication
{
    public Guid VacancyId { get; init; }
    public Guid UserApplicationId { get; init; }
    public Guid UserId { get; init; }
    public string? Text { get; init; }
    public DateTime Date { get; init; } = DateTime.UtcNow;
}