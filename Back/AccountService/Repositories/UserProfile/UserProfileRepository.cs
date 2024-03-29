using AccountService.Models;
using MongoDB.Entities;

namespace AccountService.Repositories.UserProfile;

public class UserProfileRepository : IUserProfileRepository
{
    public UserProfileRepository()
    {
        
    }


    public async Task AddUserProfile(Models.UserProfile userProfile)
    {
        await DB.InsertAsync(userProfile);
        await DB.SaveAsync(userProfile);
    }
    
    
    public async Task UpdateUserProfile(Guid id, Models.UserProfile updatedUserProfile)
    {
        var userProfile = await DB.Find<Models.UserProfile>().OneAsync(id);
        if (userProfile != null)
        {
            userProfile.City = updatedUserProfile.City;
            userProfile.Country = updatedUserProfile.Country;
            userProfile.Contacts = updatedUserProfile.Contacts;
            userProfile.About = updatedUserProfile.About;
            userProfile.Avatar = updatedUserProfile.Avatar;

            await DB.SaveAsync(userProfile);
        }
        else
        {
            throw new Exception("User profile not found");
        }
    }
    
    
    public async Task<Models.UserProfile?> GetUserProfile(Guid id)
    {
        return await DB.Find<Models.UserProfile>().OneAsync(id);
    }
    
    
    public async Task DeleteUserProfile(Guid id) => 
        await DB.DeleteAsync<Models.UserProfile>(id);
}