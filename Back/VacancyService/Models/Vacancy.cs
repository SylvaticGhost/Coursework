using MongoDB.Entities;

namespace VacancyService.Models;

public class Vacancy : Entity
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public Guid CompanyId { get; set; }
    
    public string CompanyName { get; set; }
    
    public string CompanyAddress { get; set; }
    
    public string CompanyEmail { get; set; }
    
    
}