using GlobalModels;

namespace AccountService.Models;

public record UserProfileToUpdateDto(
    string? City,
    string Country,
    List<Contact>? Contacts,
    string? About,
    byte[]? Avatar,
    string? FirstName,
    string? LastName) 
{
    public string? City { get; set; } = City;
    public string Country { get; set; } = Country;
    public List<Contact>? Contacts { get; set; } = Contacts;
    public string? About { get; set; } = About;
    public byte[]? Avatar { get; set; } = Avatar;

    public string? FirstName { get; set; } = FirstName;

    public string? LastName { get; set; } = LastName;
}