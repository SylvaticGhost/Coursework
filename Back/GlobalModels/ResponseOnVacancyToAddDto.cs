namespace GlobalModels;

public record ResponseOnVacancyToAddDto(
    Guid VacancyId,
    ShortResume Resume
    );