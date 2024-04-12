namespace VacancyService.Models;

public record SearchVacancyParams
{
    public Guid? VacancyId { get; set; }
    public Guid? CompanyId { get; set; }
    public string? Title { get; set; }
    
    public string? Specialization { get; set; }

    public bool CombineTextParam { get; set; } = false;
}