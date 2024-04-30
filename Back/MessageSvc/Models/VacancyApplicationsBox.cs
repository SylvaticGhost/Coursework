using GlobalModels.Messages;
using MongoDB.Entities;

namespace MessageSvc.Models;

public class VacancyApplicationsBox : Entity
{
    public Guid VacancyId { get; }
    public Guid CompanyId { get; }
    public List<UserApplicationOnVacancy> UserApplications { get; }
    
    public VacancyApplicationsBox(Guid vacancyId, Guid companyId)
    {
        VacancyId = vacancyId;
        CompanyId = companyId;
        UserApplications = new List<UserApplicationOnVacancy>();
    }
    
    
    public void AddApplication(UserApplicationOnVacancy? application)
    {
        ArgumentNullException.ThrowIfNull(application);

        UserApplications.Add(application);
    }
    
    
    public void DeleteApplication(Guid applicationId)
    {
        UserApplicationOnVacancy? application = UserApplications.FirstOrDefault(a => a.UserApplicationId == applicationId);
        
        ArgumentNullException.ThrowIfNull(application);
        
        UserApplications.Remove(application);
    }
}