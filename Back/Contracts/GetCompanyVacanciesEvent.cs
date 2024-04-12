namespace Contracts;

public record GetCompanyVacanciesEvent(
    Guid CompanyId)
{
    public DateTime Time { get; } = DateTime.UtcNow;   
}