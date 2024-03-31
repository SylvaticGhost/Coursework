using System.ComponentModel.DataAnnotations;
using MongoDB.Entities;

namespace VacancyService.Models;

public class Vacancy : Entity
{
    [Required]
    public Guid Id { get; set; }

    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public CompanyShortInfo CompanyInfo { get; set; }
    
    
}