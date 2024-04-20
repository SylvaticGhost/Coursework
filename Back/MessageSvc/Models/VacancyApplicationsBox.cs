using GlobalModels.Messages;
using MongoDB.Entities;

namespace MessageSvc.Models;

public class VacancyApplicationsBox : Entity
{
    public Guid VacancyId { get; }
    public List<UserApplicationOnVacancy> UserApplications { get; }
    
    public VacancyApplicationsBox(Guid vacancyId)
    {
        VacancyId = vacancyId;
        UserApplications = new List<UserApplicationOnVacancy>();
    }
}