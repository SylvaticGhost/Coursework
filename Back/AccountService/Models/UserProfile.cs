using System.ComponentModel.DataAnnotations;
using GlobalModels;
using MongoDB.Entities;

namespace AccountService.Models;

public class UserProfile : Entity
{
    [Required]
    public Guid Id { get; set; }
    public string? City { get; set; }
    public string Country { get; set; }
    public IEnumerable<Contact>? Contacts { get; set; }
    public string? About { get; set; }
    public byte[]? Avatar { get; set; }

    public UserProfile(UserProfileToAddDto userProfileToAddDto)
    {
        Id = Guid.NewGuid();
        City = userProfileToAddDto.City;
        Country = userProfileToAddDto.Country;
        Contacts = userProfileToAddDto.Contacts;
        About = userProfileToAddDto.About;
        Avatar = userProfileToAddDto.Avatar ?? GetDefaultAvatar();
    }
    
    public static byte[] GetDefaultAvatar() => File.ReadAllBytes("Media/UserDefaultAvatar.png");
}

