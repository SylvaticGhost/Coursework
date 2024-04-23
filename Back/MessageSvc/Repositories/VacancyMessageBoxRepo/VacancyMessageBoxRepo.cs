﻿using GlobalModels.Messages;
using GlobalModels.Vacancy;
using MassTransit;
using MessageSvc.Models;
using MongoDB.Driver;
using MongoDB.Entities;

namespace MessageSvc.Repositories.VacancyMessageBoxRepo;

public class VacancyMessageBoxRepo : IVacancyMessageBoxRepo
{
    public async Task CreateMessageBox(Guid vacancyId, Guid companyId)
    {
        VacancyApplicationsBox box = new(vacancyId, companyId);

        await box.SaveAsync();
    }
    
    
    public async Task<bool> CheckIfVacancyHasApplicationBox(Guid vacancyId) =>
        await DB.Collection<VacancyApplicationsBox>().Find(b => b.VacancyId == vacancyId).AnyAsync();
    
    
    public async Task<bool> CheckIfUserHasApplication(Guid vacancyId, Guid userId) =>
        await DB.Collection<VacancyApplicationsBox>()
            .Find(b => b.VacancyId == vacancyId && b.UserApplications
                .Any(a => a.UserId == userId))
            .AnyAsync();


    public async Task AddApplication(UserApplicationOnVacancy application)
    {
        VacancyApplicationsBox? box = await DB.Find<VacancyApplicationsBox>()
            .Match(b => b.VacancyId == application.VacancyId)
            .ExecuteSingleAsync();
        
        ArgumentNullException.ThrowIfNull(box);
        
        box.AddApplication(application);
        
        await box.SaveAsync();
    }


    public async Task<IEnumerable<UserApplicationOnVacancy>> GetUserApplications(Guid userId)
    {
        var boxes = await DB.Find<VacancyApplicationsBox>()
            .Match(b => b.UserApplications.Any(a => a.UserId == userId))
            .ExecuteAsync();
        
        return boxes.SelectMany(b => b.UserApplications.Where(a => a.UserId == userId));
    }
}