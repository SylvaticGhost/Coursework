using GlobalModels.Messages;

namespace MessageSvc.Repositories.VacancyMessageBoxRepo;

public interface IVacancyMessageBoxRepo
{
    public Task CreateMessageBox(Guid vacancyId, Guid companyId);
    
    public Task<bool> CheckIfVacancyHasApplicationBox(Guid vacancyId);
    
    public Task<bool> CheckIfUserHasApplication(Guid vacancyId, Guid userId);
    
    public Task AddApplication(UserApplicationOnVacancy application);
    
    public Task<IEnumerable<UserApplicationOnVacancy>> GetUserApplications(Guid userId);
    
    public Task<UserApplicationOnVacancy?> GetUserApplicationOnVacancy(Guid vacancyId, Guid userId);
    
    public Task DeleteApplications(params Guid[] applicationIds);

    public Task DeleteApplication(Guid vacancyId, Guid userId);
    
    public Task<bool> CheckIfUserApplied(Guid vacancyId, Guid userId);
    
    public Task<Guid> GetUserIdFromApplication(Guid applicationId, Guid vacancyId);
}