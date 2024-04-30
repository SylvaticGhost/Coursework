using System.ComponentModel.DataAnnotations;
using MongoDB.Entities;
using VacancyService.Models;

namespace GlobalModels.Vacancy;

public class Vacancy : Entity, IVacancyInputFields
{
    [Required]
    public Guid VacancyId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    
    public string Salary { get; set; }
    
    public string Experience { get; set; }
    
    public string Specialization { get; set; }
    
    public DateTime CreatedAt { get; set; } 
    
    public DateTime UpdatedAt { get; set; }
    [Required]
    public CompanyShortInfo CompanyInfo { get; set; }
    
    
}