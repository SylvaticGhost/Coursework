namespace MessageSvc.Repositories.UserMessageBoxRepo;

public interface IUserMessageBoxRepo
{
    public Task CreateUserMessageBox(Guid userId);
    
    public Task<bool> UserMessageBoxExists(Guid userId);
}