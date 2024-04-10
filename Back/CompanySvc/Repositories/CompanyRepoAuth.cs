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
        
        if(companyAuthCrypted.HasNullProperties())
            throw new Exception("Company auth crypted has null properties");
        
        try
        {
            await companyAuthCrypted.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
       
        return key;
    }


    public bool CheckIfCompanyHasAuth(Guid companyId) =>
        DB.Collection<CompanyAuthCrypted>().Find(c => c.CompanyId == companyId).Any();

    
    public async Task<bool> CompanyLogin(CompanyToLoginDto company)
    {
        CompanyAuthCrypted companyAuthCrypted = await DB.Collection<CompanyAuthCrypted>()
            .Find(c => c.CompanyId == company.CompanyId).FirstOrDefaultAsync();

        Console.WriteLine("companyAuthCrypted: " + companyAuthCrypted);
        if (companyAuthCrypted == null)
        {
            throw new Exception("Company auth crypted is null");
            return false;
        }
        
        if (company.Key == null)
            return false;
        //Console.WriteLine("company.Key: " + company.Key, "len of salt: " + companyAuthCrypted.KeySalt.Length, "len of hash: " + companyAuthCrypted.KeyHash.Length);
        return GlobalAuthHelpers.VerifyPasswordHash(company.Key.ToString(), companyAuthCrypted.KeyHash, companyAuthCrypted.KeySalt);
    }
}