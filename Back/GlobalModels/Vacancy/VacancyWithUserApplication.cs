using GlobalModels.Messages;

namespace GlobalModels.Vacancy;

public class VacancyWithUserApplication(
    VacancyDto Vacancy,
    UserApplicationOnVacancy ResponseOnVacancy
)
{
    public static IEnumerable<VacancyWithUserApplication> FromVacancyAndUserApplications(
        IEnumerable<VacancyDto> vacancies,
        IEnumerable<UserApplicationOnVacancy> userApplications
    )
    {
        return vacancies.Select(vacancy => new VacancyWithUserApplication(
            vacancy,
            userApplications.FirstOrDefault(userApplication => userApplication.VacancyId == vacancy.VacancyId)
        ));
    }
}