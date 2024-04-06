namespace AccountService.Repositories;

public interface IUserRepositoryBasic
{
    public Task<(string? FirstName, string? LastName)> GetNameAndSurname(Guid id);
}