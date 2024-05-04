namespace GlobalModels.Messages.CompanyResponse;

public record AnswerOnApplication
{
    public Guid VacancyId { get; init; }
    public Guid CompanyId { get; init; }
    public string CompanyName { get; init; }
    public Guid UserApplicationId { get; init; }
    public Guid UserId { get; init; }
    public string? Text { get; init; }
    public DateTime Date { get; init; } = DateTime.UtcNow;

    public AnswerOnApplication(AnswerOnApplicationToAddDto answer, Guid companyId, string companyName, Guid userId)
    {
        VacancyId = answer.VacancyId;
        CompanyId = companyId;
        CompanyName = companyName;
        UserApplicationId = answer.UserApplicationId;
        Text = answer.Text;
        UserId = userId;
    }
}