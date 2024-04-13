namespace Contracts;

public record UpdateCompanyEvent
(
    Guid CompanyId,
    string Name,
    string Address,
    string Email,
    string PhoneNumber,
    string Website,
    byte[]? Logo,
    int NumberOfEmployees,
    string Description,
    string Industry,
    DateTime UpdateAt
);