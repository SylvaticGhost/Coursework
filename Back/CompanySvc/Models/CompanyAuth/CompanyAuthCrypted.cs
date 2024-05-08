using MongoDB.Bson;
using MongoDB.Entities;

namespace CompanySvc.Models;

public class CompanyAuthCrypted : Entity
{
    public Guid CompanyId { get; init; }
    
    public byte[]? KeyHash { get; init; }

    public byte[]? KeySalt { get; init; }
    
    public CompanyAuthCrypted(Guid companyId, byte[] keyHash, byte[] keySalt)
    {
        ID = ObjectId.GenerateNewId().ToString();
        CompanyId = companyId;
        KeyHash = keyHash;
        KeySalt = keySalt;
    }


    public override string ToString()
    {
        return $"CompanyId: {CompanyId}, KeyHash: {KeyHash}, KeySalt: {KeySalt}";
    }
    
    
    public bool HasNullProperties()
    {
        return CompanyId == Guid.Empty || KeyHash == null || KeySalt == null;
    }
}