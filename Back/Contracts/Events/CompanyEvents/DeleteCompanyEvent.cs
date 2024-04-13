namespace Contracts;

public record DeleteCompanyEvent(
    Guid CompanyId,
    DateTime DeletedAt
    );
