namespace VacancyService.Models;

public record SearchVacancyParams
{
    public Guid? VacancyId { get; init; }
    public Guid? CompanyId { get; init; }
    public string? Title { get; init; }
    
    public string? Description { get; init; }
    
    public string? CompanyName { get; init; }
    
    public string? Specialization { get; init; }

    public bool CombineTextParam { get; init; } = false;
}