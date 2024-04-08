using MongoDB.Entities;

namespace CompanySvc.Models;

public class CompanyAuthCrypted : Entity
{
    public Guid CompanyId { get; set; }
    
    public byte[] KeyCrypted { get; set; }

    public byte[] KeySalt { get; set; }
    
    public CompanyAuthCrypted(Guid companyId, byte[] keyCrypted, byte[] keySalt)
    {
        CompanyId = companyId;
        KeyCrypted = keyCrypted;
        KeySalt = keySalt;
    }
}