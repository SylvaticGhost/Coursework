using MessageSvc.Models;
using MongoDB.Entities;

namespace MessageSvc.Repositories.VacancyMessageBoxRepo;

public class VacancyMessageBoxRepo : IVacancyMessageBoxRepo
{
    public async Task CreateMessageBox(Guid companyId)
    {
        VacancyApplicationsBox box = new(companyId);

        await box.SaveAsync();
    }
}