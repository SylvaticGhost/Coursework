using MongoDB.Entities;

namespace CompanySvc.Models;

public class Company : Entity
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Address { get; set; }
}