using CompanySvc.Models;
using Contracts;
using MassTransit;
using MassTransit.RabbitMqTransport;
using MongoDB.Driver;
using MongoDB.Entities;
using VacancyService.Models;
using Guid = System.Guid;

namespace VacancyService.Repositories;

public class CompanyRepo : ICompanyRepo
{
    private readonly IPublishEndpoint _publisher;
    private readonly IMongoCollection<Company> _collection = DB.Collection<Company>();
    
    public CompanyRepo(IPublishEndpoint publisher)
    {
        _publisher = publisher;
    }
    
    public async Task<Company?> GetCompanyById(Guid id) => 
        await DB.Find<Company>().OneAsync(id);
    
    
    public async Task<Company?> GetCompanyByName(string name)
    {
        var collection = DB.Collection<Company>();
        return await collection.Find(c => c.Name == name).FirstOrDefaultAsync();
    }
    
    
    public async Task<bool> CheckIfCompanyExists(Guid id) =>
        await _collection.Find(c => c.CompanyId == id).AnyAsync();


    public async Task<CompanyShortInfo?> GetCompanyShortInfoById(Guid id)
    {
        var collection = DB.Collection<Company>();
        
        var projection = Builders<Company>.Projection.Expression(c => new CompanyShortInfo
        {
            CompanyId = c.CompanyId,
            Name = c.Name,
            Address = c.Address,
            CompanyEmail = c.Email,
            PhoneNumber = c.PhoneNumber
        });
        
        return await collection.Find(c => c.CompanyId == id)
            .Project(projection)
            .FirstOrDefaultAsync();
    }
    
    
    public async Task CreateCompany(CompanyToAddDto companyToAddDto)
    {
        var company = new Company
        {
            CompanyId = Guid.NewGuid(),
            Name = companyToAddDto.Name,
            Address = companyToAddDto.Address,
            Email = companyToAddDto.Email,
            PhoneNumber = companyToAddDto.PhoneNumber,
            Website = companyToAddDto.Website,
            Logo = companyToAddDto.Logo,
            CreatedAt = DateTime.Now,
            Description = companyToAddDto.Description,
            Industry = companyToAddDto.Industry
        };
        
        await company.SaveAsync();
    }


    public async Task UpdateCompany(CompanyToUpdateDto company)
    {
        var filter = Builders<Company>.Filter.Eq(c => c.CompanyId, company.CompanyId);
        var update = Builders<Company>.Update
            .Set(c => c.Name, company.Name)
            .Set(c => c.Address, company.Address)
            .Set(c => c.Email, company.Email)
            .Set(c => c.PhoneNumber, company.PhoneNumber)
            .Set(c => c.Website, company.Website)
            .Set(c => c.Logo, company.Logo)
            .Set(c => c.NumberOfEmployees, company.NumberOfEmployees)
            .Set(c => c.Description, company.Description)
            .Set(c => c.Industry, company.Industry);
        
        await _collection.UpdateManyAsync(filter, update);
        
        var updateCompanyEvent = new UpdateCompanyEvent(
                                CompanyId: company.CompanyId,
                                Name: company.Name,
                                Address: company.Address,
                                Email: company.Email,
                                PhoneNumber: company.PhoneNumber,
                                Website: company.Website,
                                Logo: company.Logo,
                                NumberOfEmployees: company.NumberOfEmployees,
                                Description: company.Description,
                                Industry: company.Industry,
                                UpdateAt: DateTime.Now
        );

        await _publisher.Publish(updateCompanyEvent);
    }
    
    
    public async Task DeleteCompany(Guid id)
    {
        await _collection.DeleteOneAsync(c => c.CompanyId == id);
        
        var deleteCompanyEvent = new DeleteCompanyEvent(id, DateTime.Now);
        
        await _publisher.Publish(deleteCompanyEvent);
    }
}