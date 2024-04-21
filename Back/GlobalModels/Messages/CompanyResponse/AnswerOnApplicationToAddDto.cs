namespace GlobalModels.Messages.CompanyResponse;

public record AnswerOnApplicationToAddDto(
    Guid UserApplicationId,
    string? Text
    );