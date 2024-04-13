using GlobalModels;

namespace GlobalModels;

public record ResponseOnVacancy(
    Guid UserId,
    Guid VacancyId,
    ShortResume Resume,
    DateTime Date
    );