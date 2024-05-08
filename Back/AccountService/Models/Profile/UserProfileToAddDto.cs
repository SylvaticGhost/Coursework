using GlobalModels;

namespace AccountService.Models;

public record UserProfileToAddDto(
    string? City,
    string Country,
    List<Contact>? Contacts,
    string? About,
    byte[]? Avatar
    );