using System.ComponentModel.DataAnnotations;
using GlobalModels;
using MongoDB.Entities;

namespace AccountService.Models;

public class UserProfile : Entity
{
    [Required]
    public Guid UserId { get; set; }
    public string? City { get; set; }
    public string Country { get; set; }
    public List<Contact>? Contacts { get; set; }
    public string? About { get; set; }
    public byte[]? Avatar { get; set; }

    public UserProfile(UserProfileToAddDto userProfileToAddDto, Guid userId)
    {
        UserId = userId;
        City = userProfileToAddDto.City;
        Country = userProfileToAddDto.Country;
        Contacts = userProfileToAddDto.Contacts ?? new List<Contact>();
        About = userProfileToAddDto.About;
        Avatar = userProfileToAddDto.Avatar ?? GetDefaultAvatar();
    }
    
    public static byte[] GetDefaultAvatar() => File.ReadAllBytes("Media/UserDefaultAvatar.png");
}

