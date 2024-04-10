using MongoDB.Entities;

namespace CompanySvc.Models;

public class CompanyAuth: Entity
{
    public  Guid CompanyId { get; set; }
    public string Key { get; set; }

    public CompanyAuth(Guid companyId, string key)
    {
        CompanyId = companyId;
        Key = key;
    }
}