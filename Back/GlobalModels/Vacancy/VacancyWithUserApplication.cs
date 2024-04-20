using GlobalModels.Messages;

namespace GlobalModels.Vacancy;

public record VacancyWithUserApplication(
    Vacancy Vacancy,
    UserApplicationOnVacancy ResponseOnVacancy
    );