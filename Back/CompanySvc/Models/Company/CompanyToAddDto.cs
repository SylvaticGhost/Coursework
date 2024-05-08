namespace CompanySvc.Models;

public record CompanyToAddDto(
    string Name,
    string Email,
    string? Address,
    string PhoneNumber,
    string? Website,
    byte[]? Logo,
    string Description,
    string? Industry
    );