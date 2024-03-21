using AccountService.Data;
using AccountService.Helpers;
using AccountService.Models;
using AccountService.Models.HelpersModels;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContextEf _dataContextEf;
    private readonly AuthHelpers _authHelpers;

    public UserRepository(DataContextEf dataContextEf, IConfiguration configuration)
    {
        _dataContextEf = dataContextEf;
        _authHelpers = new AuthHelpers(configuration);
    }


    public async Task<Guid> AddUser(UserAccountToAddDto userAccountToAddDto)
    {
        HashedPasswords password = AuthHelpers.CreatePasswordHash(userAccountToAddDto.Password);
        
        UserAccount userAccount = new()
        {
            FirstName = userAccountToAddDto.FirstName,
            LastName = userAccountToAddDto.LastName,
            Email = userAccountToAddDto.Email,
            PhoneNumber = userAccountToAddDto.PhoneNumber,
            DateOfBirth = userAccountToAddDto.DateOfBirth,
            PasswordHash = password.PasswordHash,
            PasswordSalt = password.PasswordSalt
        };
        
        await _dataContextEf.UserAccount.AddAsync(userAccount);
        await _dataContextEf.SaveChangesAsync();

        return userAccount.Id;
    }
    
    
    public async Task<UserAccount?> GetUserByEmail(string email) => 
        await _dataContextEf.UserAccount.FirstOrDefaultAsync(x => x.Email == email);
    
    public async Task<UserAccount?> GetUserById(Guid id) =>
        await _dataContextEf.UserAccount.FirstOrDefaultAsync(x => x.Id == id);
    
    public async Task<UserAccount?> GetUserByPhoneNumber(string phoneNumber) =>
        await _dataContextEf.UserAccount.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
    
    
    public async Task<string> Login(string credential, string password, TypeOfLogin typeOfLogin)
    {
        UserAccount? user;
        if (typeOfLogin == TypeOfLogin.LoginByEmail)
            user = await GetUserByEmail(credential);
        else
            user = await GetUserByPhoneNumber(credential);
        
        if (user == null)
            throw new ArgumentException("User not found");
        
        if(string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password is required");
        
        if (!AuthHelpers.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new ArgumentException("Invalid password");
        
        string token = _authHelpers.GenerateJwtToken(user);
        
        return token;
    }
}