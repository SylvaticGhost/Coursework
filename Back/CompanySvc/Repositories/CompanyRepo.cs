using CompanySvc.Models;
using MongoDB.Driver;
using MongoDB.Entities;
using VacancyService.Models;
using Guid = System.Guid;

namespace DefaultNamespace;

public class CompanyRepo : ICompanyRepo
{
    public CompanyRepo()
    {
        
    }
    
    public async Task<Company?> GetCompanyById(Guid id) => 
        await DB.Find<Company>().OneAsync(id);
    
    
    public async Task<Company?> GetCompanyByName(string name)
    {
        var collection = DB.Collection<Company>();
        return await collection.Find(c => c.Name == name).FirstOrDefaultAsync();
    }


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
}