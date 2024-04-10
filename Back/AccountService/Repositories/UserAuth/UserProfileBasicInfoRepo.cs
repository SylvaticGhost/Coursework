using AccountService.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Repositories;

public class UserProfileBasicInfoRepo : IUserRepositoryBasic
{
    private readonly DataContextNpgEf _dataContextNpgEf;
    
    public UserProfileBasicInfoRepo(DataContextNpgEf dataContextNpgEf)
    {
        _dataContextNpgEf = dataContextNpgEf;
    }
   
    
    public async Task<(string FirstName, string? LastName)> GetNameAndSurname(Guid id)
    {
        return await _dataContextNpgEf.UserAccount
            .Where(x => x.Id == id)
            .Select(x => new {x.FirstName, x.LastName})
            .FirstOrDefaultAsync()
            .ContinueWith(x => (x.Result!.FirstName, x.Result.LastName));
    }
}