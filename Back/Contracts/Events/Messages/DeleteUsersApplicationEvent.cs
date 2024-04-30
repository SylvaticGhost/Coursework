namespace Contracts.Events.Messages;

public record DeleteUsersApplicationEvent
{
    public Guid UserId { get; init; }

    public List<Guid> ApplicationId { get; init; } = [];
    
    public DateTime Date { get; init; } = DateTime.Now;
    
    public DeleteUsersApplicationEvent(Guid userId, List<Guid> applicationId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(userId));
        
        UserId = userId;
        ApplicationId = applicationId;
    }
    
    public DeleteUsersApplicationEvent(Guid userId, Guid applicationId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(userId));
        
        UserId = userId;
        ApplicationId.Add(applicationId);
    }

    public void AddApplicationId(params Guid[] applicationId) => ApplicationId.AddRange(applicationId);
    
    public void AddApplicationId(IEnumerable<Guid> applicationId) => ApplicationId.AddRange(applicationId);
}