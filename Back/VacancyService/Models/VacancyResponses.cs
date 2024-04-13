using GlobalModels;
using MongoDB.Entities;

namespace VacancyService.Models;

public sealed class VacancyResponses : Entity
{
    public Guid VacancyId { get; init; }
    
    public List<ResponseOnVacancy>? Responses { get; init; } 
    
    public VacancyResponses(Guid vacancyId)
    {
        VacancyId = vacancyId;
        Responses = new();
    }
  
}