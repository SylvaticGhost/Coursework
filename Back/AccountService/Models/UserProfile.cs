using MongoDB.Entities;

namespace AccountService.Models;

public class UserProfile : Entity
{
    public Guid Id { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Contacts { get; set; }
}