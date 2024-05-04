namespace GlobalModels.Messages.CompanyResponse;

public record AnswerOnApplicationToAddDto(
    Guid UserApplicationId,
    Guid VacancyId,
    string? Text
    );