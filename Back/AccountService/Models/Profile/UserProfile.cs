using System.ComponentModel.DataAnnotations;
using AccountService.Helpers;
using GlobalHelpers;
using GlobalModels;
using MongoDB.Entities;

namespace AccountService.Models;

public class UserProfile : Entity
{
    [Required]
    public Guid UserId { get; private set; }
    public string? City { get; private set; }
    public string Country { get; private set; }
    public List<Contact>? Contacts { get;private set; }
    public string? About { get;private set; }
    public byte[]? Avatar { get;private set; }
    
    public string? FirstName { get;private set; }
    
    public string? LastName { get;private set; }

    public UserProfile(UserProfileToAddDto userProfileToAddDto,(string?, string?) names ,Guid userId)
    {
        UserId = userId;
        City = userProfileToAddDto.City;
        Country = userProfileToAddDto.Country;
        Contacts = userProfileToAddDto.Contacts ?? new List<Contact>();
        About = userProfileToAddDto.About;
        Avatar = userProfileToAddDto.Avatar ?? GetDefaultAvatar();
        FirstName = names.Item1 ?? "";
        LastName = names.Item2 ?? "";
    }
    
    public void UpdateProfile(UserProfileToUpdateDto updatedUserProfile)
    {
        if(!Validation.CheckIfWord(FirstName))
            throw new ArgumentException("Invalid first name");
        
        if(!Validation.CheckIfWord(LastName))
            throw new ArgumentException("Invalid last name");
        
        City = updatedUserProfile.City;
        Country = updatedUserProfile.Country ?? throw new ArgumentNullException(nameof(updatedUserProfile.Country));
        Contacts = updatedUserProfile.Contacts;
        About = updatedUserProfile.About;
        Avatar = updatedUserProfile.Avatar;
        FirstName = updatedUserProfile.FirstName;
        LastName = updatedUserProfile.LastName;
    }
    
    public static byte[] GetDefaultAvatar() => File.ReadAllBytes("Media/UserDefaultAvatar.png");
}

