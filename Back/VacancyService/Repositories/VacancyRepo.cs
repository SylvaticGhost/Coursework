using GlobalModels.Vacancy;
using MongoDB.Driver;
using MongoDB.Entities;
using VacancyService.Models;

namespace VacancyService.Repositories;

public class VacancyRepo : IVacancyRepo
{
    private readonly IMongoCollection<Vacancy> _collection = DB.Collection<Vacancy>();

    public async Task<Guid> AddVacancy(VacancyToAddDto vacancy, CompanyShortInfo companyInfo, DateTime time = default)
    {
        Guid id = Guid.NewGuid();
        
        if(time == default)
            time = DateTime.UtcNow;

        var vacancyToAdd = new Vacancy
        {
            VacancyId = id,
            Title = vacancy.Title,
            Description = vacancy.Description,
            CreatedAt = time,
            UpdatedAt = time,
            CompanyInfo = companyInfo,
            Experience = vacancy.Experience,
            Salary = vacancy.Salary,
            Specialization = vacancy.Specialization
        };

        await vacancyToAdd.SaveAsync();
        

        return id;
    }


    public async Task<Vacancy?> GetVacancy(Guid id)
    {
        return await _collection.Find(v => v.VacancyId == id).FirstOrDefaultAsync();
    }


    public async Task<Guid> GetOwnerOfVacancy(Guid vacancyId) =>
        await GetVacancy(vacancyId) switch
        {
            null => Guid.Empty,
            { } vacancy => vacancy.CompanyInfo.CompanyId
        };


    public async Task<bool> CheckIfVacancyExists(Guid id) =>
        await _collection.Find(v => v.VacancyId == id).AnyAsync();
    
    
    public async Task<bool> CheckIfCompanyOwnVacancy(Guid companyId, Guid vacancyId) =>
        await _collection.Find(v => v.CompanyInfo.CompanyId == companyId && v.VacancyId == vacancyId).AnyAsync();
   
    
    public async Task UpdateCompanyInfoInVacancies(CompanyShortInfo companyInfo)
    {
        var filter = Builders<Vacancy>.Filter.Eq(v => v.CompanyInfo.CompanyId, companyInfo.CompanyId);
        var update = Builders<Vacancy>.Update.Set(v => v.CompanyInfo, companyInfo);

        await _collection.UpdateManyAsync(filter, update);
    }
    
    
    public async Task UpdateVacancy(VacancyToUpdateDto vacancy, DateTime time = default)
    {
        if(time == default)
            time = DateTime.UtcNow;
        
        var filter = Builders<Vacancy>.Filter.Eq(v => v.VacancyId, vacancy.VacancyId);
        var update = Builders<Vacancy>.Update
            .Set(v => v.Title, vacancy.Title)
            .Set(v => v.Description, vacancy.Description)
            .Set(v => v.Salary, vacancy.Salary)
            .Set(v => v.Experience, vacancy.Experience)
            .Set(v => v.Specialization, vacancy.Specialization)
            .Set(v => v.UpdatedAt, time);

        await _collection.UpdateOneAsync(filter, update);
    }


    public async Task DeleteVacancy(Guid id) =>
        await _collection.DeleteOneAsync(v => v.VacancyId == id);
    
    
    public async Task DeleteCompanyVacancies(Guid companyId) =>
        await _collection.DeleteManyAsync(v => v.CompanyInfo.CompanyId == companyId);
}