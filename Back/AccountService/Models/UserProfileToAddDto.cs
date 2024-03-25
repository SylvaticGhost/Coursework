using GlobalModels;

namespace AccountService.Models;

public record UserProfileToAddDto(
    string? City,
    string Country,
    IEnumerable<Contact>? Contacts,
    string? About,
    byte[]? Avatar
    );