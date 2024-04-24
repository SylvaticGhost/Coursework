﻿using GlobalModels.Messages;

namespace MessageSvc.Repositories.VacancyMessageBoxRepo;

public interface IVacancyMessageBoxRepo
{
    public Task CreateMessageBox(Guid vacancyId, Guid companyId);
    
    public Task<bool> CheckIfVacancyHasApplicationBox(Guid vacancyId);
    
    public Task<bool> CheckIfUserHasApplication(Guid vacancyId, Guid userId);
    
    public Task AddApplication(UserApplicationOnVacancy application);
    
    public Task<IEnumerable<UserApplicationOnVacancy>> GetUserApplications(Guid userId);
}