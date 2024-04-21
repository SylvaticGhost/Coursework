using MessageSvc.Models;
using MongoDB.Entities;

namespace MessageSvc.Repositories.UserMessageBoxRepo;

public class UserMessageBoxRepo : IUserMessageBoxRepo
{
    public async Task CreateUserMessageBox(Guid userId)
    {
        var userMessageBox = new UserMessageBox(userId);
        await userMessageBox.SaveAsync();
    }
    
    
    public async Task<bool> UserMessageBoxExists(Guid userId)
    {
        return await DB.Find<UserMessageBox>().Match(u => u.UserId == userId).ExecuteAnyAsync();
    }
}