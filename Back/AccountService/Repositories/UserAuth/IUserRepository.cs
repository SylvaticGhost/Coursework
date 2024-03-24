using AccountService.Models;

namespace AccountService.Repositories;

public interface IUserRepository
{
    public Task<Guid> AddUser(UserAccountToAddDto userAccountToAddDto);

    public Task<string> Login(string credential, string password, TypeOfLogin typeOfLogin);

    public Task<string> RefreshToken(Guid id);

    public Task<bool> DeleteUser(Guid id);

    public Task<bool> CheckIfEmailExists(string email);
    
    public Task<bool> CheckIfPhoneNumberExists(string phoneNumber);
}