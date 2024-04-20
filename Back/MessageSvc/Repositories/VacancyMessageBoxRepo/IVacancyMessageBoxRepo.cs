namespace MessageSvc.Repositories.VacancyMessageBoxRepo;

public interface IVacancyMessageBoxRepo
{
    public Task CreateMessageBox(Guid companyId);
}