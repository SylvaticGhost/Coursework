using GlobalModels.Messages.CompanyResponse;

namespace MessageSvc.Repositories.UserMessageBoxRepo;

public interface IUserMessageBoxRepo
{
    public Task CreateUserMessageBox(Guid userId);
    
    public Task<bool> UserMessageBoxExists(Guid userId);
    
    public Task<IEnumerable<AnswerOnApplication>> GetAnswersForUser(Guid userId);
    
    public Task DeleteAnswer(Guid userId, Guid applicationId);
    
    public Task DeleteBox(Guid userId);
}