using MongoDB.Entities;

namespace VacancyService.Models;

public class VacancyStatistic : Entity
{
    public Guid VacancyId { get; private set; }
    public int Views { get; private set; }
    public int Likes { get; private set; }
    
    public VacancyStatistic(Guid vacancyId)
    {
        if(vacancyId == Guid.Empty)
            throw new ArgumentException("VacancyId cannot be empty", nameof(vacancyId));
        
        VacancyId = vacancyId;
        Views = 0;
        Likes = 0;
    }
    
    public void AddView() => Views++;
    
    public void AddLike() => Likes++;
    
    public void RemoveLike() => Likes--;
}