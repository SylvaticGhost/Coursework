namespace Contracts.Events.Messages;

public enum IdType
{
    ApplicationId,
    VacancyId
}

public class DeleteUsersApplicationEvent
{
    public Guid UserId { get; init; }

    public List<Guid> ApplicationId { get; init; } = [];
    
    public DateTime Date { get; init; } = DateTime.Now;

    public Guid VacancyId { get; init; }  = Guid.Empty;

    private IdType IdType { get; init; } = IdType.ApplicationId;
    
    public bool DeleteByApplicationId => IdType == IdType.ApplicationId;
    
    public bool DeleteByVacancyId => IdType == IdType.VacancyId;
    
    public DeleteUsersApplicationEvent(Guid userId, List<Guid> applicationId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(userId));
        
        UserId = userId;
        ApplicationId = applicationId;
        VacancyId = Guid.Empty;
    }
    
    public DeleteUsersApplicationEvent(Guid userId, Guid applicationId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(userId));
        
        UserId = userId;
        ApplicationId.Add(applicationId);
    }

    private DeleteUsersApplicationEvent(Guid userId, Guid secondId, IdType idType)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(userId));
        
        UserId = userId;
        IdType = idType;
        
        if (idType == IdType.ApplicationId)
        {
            ApplicationId.Add(secondId);
            VacancyId = Guid.Empty;
        }
        else
        {
            VacancyId = secondId;
            ApplicationId = [];
        }
    }
    

    public void AddApplicationId(params Guid[] applicationId) => ApplicationId.AddRange(applicationId);
    
    public void AddApplicationId(IEnumerable<Guid> applicationId) => ApplicationId.AddRange(applicationId);
    
    public static DeleteUsersApplicationEvent FromVacancyId(Guid userId, Guid vacancyId) => 
        new(userId, vacancyId, IdType.VacancyId);
    
    public static DeleteUsersApplicationEvent FromApplicationId(Guid userId, Guid applicationId) =>
        new(userId, applicationId, IdType.ApplicationId);
}