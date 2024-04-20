using MessageSvc.Models;
using MongoDB.Entities;

namespace MessageSvc.Repositories.UserMessageBoxRepo;

public class UserMessageBoxRepo : IUserMessageBoxRepo
{
    public async Task CreateUserMessageRepoBox(Guid userId)
    {
        var userMessageBox = new Models.UserMessageBox(userId);
        await userMessageBox.SaveAsync();
    }
}