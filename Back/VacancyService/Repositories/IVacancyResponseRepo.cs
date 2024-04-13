using GlobalModels;

namespace VacancyService.Repositories;

public interface IVacancyResponseRepo
{
    public Task AddResponse(Guid vacancyId, ResponseOnVacancy response);

    public Task<IEnumerable<ResponseOnVacancy>?> GetResponses(Guid vacancyId);
}