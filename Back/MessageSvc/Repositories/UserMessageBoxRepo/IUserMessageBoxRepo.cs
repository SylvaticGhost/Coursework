namespace MessageSvc.Repositories.UserMessageBoxRepo;

public interface IUserMessageBoxRepo
{
    public Task CreateUserMessageRepoBox(Guid userId);
}