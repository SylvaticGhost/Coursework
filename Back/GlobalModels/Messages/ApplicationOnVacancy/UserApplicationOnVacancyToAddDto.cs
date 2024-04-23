namespace GlobalModels.Messages;

public record UserApplicationOnVacancyToAddDto(
    Guid VacancyId,
    ShortResume ShortResume
    );