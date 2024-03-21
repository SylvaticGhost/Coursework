using AccountService.Models;

namespace AccountService.Repositories;

public interface IUserRepository
{
    public Task<Guid> AddUser(UserAccountToAddDto userAccountToAddDto);

    public Task<string> Login(string credential, string password, TypeOfLogin typeOfLogin);
}