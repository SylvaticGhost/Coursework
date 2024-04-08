using CompanySvc.Helpers;
using CompanySvc.Models;
using GlobalHelpers;
using GlobalHelpers.Models;
using MongoDB.Driver;
using MongoDB.Entities;

namespace CompanySvc.Repositories;

public class CompanyRepoAuth : ICompanyRepoAuth
{
    public async Task<Guid> AddCompanyAuth(Guid companyId)
    {
        Guid key = Guid.NewGuid();

        HashedPasswords hashedPasswords = GlobalAuthHelpers.CreatePasswordHash(key.ToString());

        CompanyAuthCrypted companyAuthCrypted =
            new CompanyAuthCrypted(companyId, hashedPasswords.PasswordHash, hashedPasswords.PasswordSalt);
        
        await DB.Collection<CompanyAuthCrypted>().InsertOneAsync(companyAuthCrypted);

        return key;
    }


    public bool CheckIfCompanyHasAuth(Guid companyId) =>
        DB.Collection<CompanyAuthCrypted>().Find(c => c.CompanyId == companyId).Any();

    
}