using System.ComponentModel.DataAnnotations;

namespace VacancyService.Models;

public class CompanyShortInfo
{
    [Required]
    public Guid CompanyId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string CompanyEmail { get; set; }
    
    public string PhoneNumber { get; set; }
}