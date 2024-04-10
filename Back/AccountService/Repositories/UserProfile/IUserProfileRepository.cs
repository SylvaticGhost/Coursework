using AccountService.Models;

namespace AccountService.Repositories.UserProfile;

public interface IUserProfileRepository
{
    public Task AddUserProfile(Models.UserProfile userProfile);
    
    public Task UpdateUserProfile(Models.UserProfile updatedUserProfile);
    
    public Task<Models.UserProfile?> GetUserProfile(Guid id);
    
    public Task DeleteUserProfile(Guid id);

    public Task<Guid> CreateUserProfile(UserProfileToAddDto userProfileToAddDto,(string, string) nameAndSurname , Guid userId);

    public Task AddContacts(Guid userId, IEnumerable<GlobalModels.Contact> contacts);
}