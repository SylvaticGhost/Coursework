using GlobalModels;
using MongoDB.Driver;
using MongoDB.Entities;
using VacancyService.Models;

namespace VacancyService.Repositories;

public class VacancyResponseRepo : IVacancyResponseRepo
{
    private readonly IMongoCollection<VacancyResponses> _vacancyResponses = DB.Collection<VacancyResponses>();
    
    public async Task AddResponse(Guid vacancyId, ResponseOnVacancy response)
    {
        var vacancyResponses = _vacancyResponses.Find(v => v.VacancyId == vacancyId).FirstOrDefault();

        if (vacancyResponses?.Responses is null)
            throw new ArgumentException("The provided vacancy hasn't a response box");
        
        vacancyResponses.Responses.Add(response);
        
        await vacancyResponses.SaveAsync();
    }
    
    
    public async Task<IEnumerable<ResponseOnVacancy>?> GetResponses(Guid vacancyId)
    {
        var vacancyResponses = await _vacancyResponses.Find(v => v.VacancyId == vacancyId).FirstOrDefaultAsync();
        return vacancyResponses?.Responses;
    }
    
}