using AccountService.Models;
using MongoDB.Driver;
using MongoDB.Entities;

namespace AccountService.Repositories.UserProfile;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly IMongoCollection<Models.UserProfile> _collection = DB.Collection<Models.UserProfile>();
    public UserProfileRepository()
    {
        
    }


    public async Task AddUserProfile(Models.UserProfile userProfile)
    {
        await DB.InsertAsync(userProfile);
        await DB.SaveAsync(userProfile);
    }
    
    
    public async Task<Guid> CreateUserProfile(UserProfileToAddDto userProfileToAddDto, Guid userId)
    {
        var userProfile = new Models.UserProfile(userProfileToAddDto, userId);
        await DB.InsertAsync(userProfile);
        return userProfile.UserId;
    }
    
    
    public async Task UpdateUserProfile(Models.UserProfile updatedUserProfile)
    {
        var userProfile = await DB.Find<Models.UserProfile>().OneAsync(updatedUserProfile.ID);
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
        return  _collection.Find(x => x.UserId == id).FirstOrDefault();
    }
    
    
    public async Task DeleteUserProfile(Guid id) => 
        await DB.DeleteAsync<Models.UserProfile>(id);
    
    
    public async Task AddContacts(Guid userId, IEnumerable<GlobalModels.Contact> contacts)
    {
        Models.UserProfile? userProfile = _collection.Find(p => p.UserId == userId).FirstOrDefault();
        if (userProfile != null)
        {
            userProfile.Contacts!.AddRange(contacts);
            await DB.SaveAsync(userProfile);
        }
        else
        {
            throw new Exception("User profile not found");
        }
    }
}