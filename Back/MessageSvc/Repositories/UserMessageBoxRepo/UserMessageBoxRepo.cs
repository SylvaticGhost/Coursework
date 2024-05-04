using GlobalModels.Messages.CompanyResponse;
using MessageSvc.Models;
using MongoDB.Driver;
using MongoDB.Entities;

namespace MessageSvc.Repositories.UserMessageBoxRepo;

public class UserMessageBoxRepo : IUserMessageBoxRepo
{
    private readonly IMongoCollection<UserMessageBox> _collection = DB.Collection<UserMessageBox>();
    
    public async Task CreateUserMessageBox(Guid userId)
    {
        var userMessageBox = new UserMessageBox(userId);
        await userMessageBox.SaveAsync();
    }
    
    
    public async Task<bool> UserMessageBoxExists(Guid userId)
    {
        return await DB.Find<UserMessageBox>().Match(u => u.UserId == userId).ExecuteAnyAsync();
    }


    public async Task<IEnumerable<AnswerOnApplication>?> GetAnswersForUser(Guid userId)
    {
        UserMessageBox? messageBox = await GetUserMessageBox(userId);

        return messageBox?.CompanyResponses;
    }
    
    
    public async Task DeleteAnswer(Guid userId, Guid applicationId)
    {
        var messageBox = await GetUserMessageBox(userId);

        ArgumentNullException.ThrowIfNull(messageBox, "MessageBox not found");
        
        var answer = messageBox.CompanyResponses.FirstOrDefault(a => a.UserApplicationId == applicationId);

        if (answer == null)
            throw new Exception("Answer not found");

        messageBox.CompanyResponses.Remove(answer);

        await messageBox.SaveAsync();
    }
    
    
    public async Task DeleteBox(Guid userId)
    {
        var messageBox = await GetUserMessageBox(userId);
        await messageBox.DeleteAsync();
    }


    private async Task<UserMessageBox?> GetUserMessageBox(Guid userId) =>
        await _collection.FindAsync(m => m.UserId == userId).Result.FirstOrDefaultAsync();

}