using Contracts;
using MassTransit;
using MongoDB.Driver;
using MongoDB.Entities;
using VacancyService.Models;

namespace VacancyService.Repositories;

public class VacancyRepo : IVacancyRepo
{
    private readonly IMongoCollection<Vacancy> _collection = DB.Collection<Vacancy>();
    public VacancyRepo()
    {
    }


    public async Task<Guid> AddVacancy(VacancyToAddDto vacancy, CompanyShortInfo companyInfo)
    {
        Guid id = Guid.NewGuid();

        var vacancyToAdd = new Vacancy
        {
            VacancyId = id,
            Title = vacancy.Title,
            Description = vacancy.Description,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            CompanyInfo = companyInfo
        };

        await vacancyToAdd.SaveAsync();

        return id;
    }


    public async Task<Vacancy?> GetVacancy(Guid id)
    {
        return await _collection.Find(v => v.VacancyId == id).FirstOrDefaultAsync();
    }


    public async Task<bool> CheckIfVacancyExists(Guid id) =>
        await _collection.Find(v => v.VacancyId == id).AnyAsync();
   
    
    public async Task UpdateCompanyInfoInVacancies(CompanyShortInfo companyInfo)
    {
        var filter = Builders<Vacancy>.Filter.Eq(v => v.CompanyInfo.CompanyId, companyInfo.CompanyId);
        var update = Builders<Vacancy>.Update.Set(v => v.CompanyInfo, companyInfo);

        await _collection.UpdateManyAsync(filter, update);
    }


    public async Task DeleteVacancy(Guid id) =>
        await _collection.DeleteOneAsync(v => v.VacancyId == id);
    
    
    public async Task DeleteCompanyVacancies(Guid companyId) =>
        await _collection.DeleteManyAsync(v => v.CompanyInfo.CompanyId == companyId);
}