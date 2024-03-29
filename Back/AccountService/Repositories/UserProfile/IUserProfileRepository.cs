namespace AccountService.Repositories.UserProfile;

public interface IUserProfileRepository
{
    public Task AddUserProfile(Models.UserProfile userProfile);
    
    public Task UpdateUserProfile(Guid id, Models.UserProfile updatedUserProfile);
    
    public Task<Models.UserProfile?> GetUserProfile(Guid id);
    
    public Task DeleteUserProfile(Guid id);
}