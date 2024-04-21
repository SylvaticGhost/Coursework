using MessageSvc.Models;
using MongoDB.Entities;

namespace MessageSvc.Repositories.VacancyMessageBoxRepo;

public class VacancyMessageBoxRepo : IVacancyMessageBoxRepo
{
    public async Task CreateMessageBox(Guid vacancyId, Guid companyId)
    {
        VacancyApplicationsBox box = new(vacancyId, companyId);

        await box.SaveAsync();
    }
}