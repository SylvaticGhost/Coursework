using GlobalModels.Messages;
using MongoDB.Entities;

namespace MessageSvc.Models;

public class VacancyApplicationsBox : Entity
{
    public Guid VacancyId { get; set; }
    public Guid CompanyId { get; set; }
    public List<UserApplicationOnVacancy>? UserApplications { get; set; }
    
    public VacancyApplicationsBox(Guid vacancyId, Guid companyId)
    {
        if(vacancyId == Guid.Empty)
            throw new ArgumentException("VacancyId is empty");
        
        if(companyId == Guid.Empty)
            throw new ArgumentException("CompanyId is empty");
        
        VacancyId = vacancyId;
        CompanyId = companyId;
        UserApplications = new List<UserApplicationOnVacancy>();
    }
    
    
    public VacancyApplicationsBox(Guid vacancyId, Guid companyId, List<UserApplicationOnVacancy>? userApplications)
    {
        if(vacancyId == Guid.Empty)
            throw new ArgumentException("VacancyId is empty");
        
        if(companyId == Guid.Empty)
            throw new ArgumentException("CompanyId is empty");
        
        ArgumentNullException.ThrowIfNull(userApplications);
        
        VacancyId = vacancyId;
        CompanyId = companyId;
        UserApplications = userApplications;
    }
    
    
    public void AddApplication(UserApplicationOnVacancy? application)
    {
        ArgumentNullException.ThrowIfNull(application);
        
        UserApplications ??= [];

        UserApplications.Add(application);
    }
    
    
    public void DeleteApplication(Guid applicationId)
    {
        
        UserApplicationOnVacancy? application = UserApplications!.FirstOrDefault(a => a.UserApplicationId == applicationId);
        
        ArgumentNullException.ThrowIfNull(application);
        
        UserApplications!.Remove(application);
    }


    public void DeleteApplicationsByUser(Guid userId) =>
        UserApplications!.RemoveAll(a => a.UserId == userId);
    
    
    public bool CheckIfUserApplied(Guid userId)
    {
        if (UserApplications is null)
            throw new ArgumentException("UserApplications is null");
        
        return UserApplications.Any(a => a.UserId == userId);
    }
}