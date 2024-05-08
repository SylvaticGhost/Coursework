namespace CompanySvc.Models;

public record CompanyToUpdateDto(
    Guid CompanyId,
    string Name,
    string Address,
    string Email,
    string PhoneNumber,
    string Website,
    byte[]? Logo,
    int NumberOfEmployees,
    string Description,
    string Industry
    );