using AccountService.Models;

namespace AccountService.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Adds a new user account to the repository.
    /// </summary>
    /// <param name="userAccountToAddDto">The user account to add.</param>
    /// <returns>The unique identifier of the added user.</returns>
    public Task<Guid> AddUser(UserAccountToAddDto userAccountToAddDto);

    /// <summary>
    /// Performs a login operation for the specified credential and password.
    /// </summary>
    /// <param name="credential">The credential used for login (e.g., email or phone number).</param>
    /// <param name="password">The password used for login.</param>
    /// <param name="typeOfLogin">The type of login (e.g., email or phone number).</param>
    /// <returns>The authentication token.</returns>
    public Task<string> Login(string credential, string password, TypeOfLogin typeOfLogin);

    /// <summary>
    /// Refreshes the authentication token for the specified user.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>The new authentication token.</returns>
    public Task<string> RefreshToken(Guid id);

    /// <summary>
    /// Deletes the user account with the specified unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>A boolean indicating whether the user account was deleted successfully.</returns>
    public Task<bool> DeleteUser(Guid id);

    /// <summary>
    /// Checks if an email address already exists in the repository.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <returns>A boolean indicating whether the email address exists.</returns>
    public Task<bool> CheckIfEmailExists(string email);
    
    /// <summary>
    /// Checks if a phone number already exists in the repository.
    /// </summary>
    /// <param name="phoneNumber">The phone number to check.</param>
    /// <returns>A boolean indicating whether the phone number exists.</returns>
    public Task<bool> CheckIfPhoneNumberExists(string phoneNumber);

    /// <summary>
    /// Retrieves a user account from the repository based on the email address.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <returns>The user account, or null if not found.</returns>
    public Task<UserAccount?> GetUserByEmail(string email);

    /// <summary>
    /// Retrieves a user account from the repository based on the phone number.
    /// </summary>
    /// <param name="phoneNumber">The phone number of the user.</param>
    /// <returns>The user account, or null if not found.</returns>
    public Task<UserAccount?> GetUserByPhoneNumber(string phoneNumber);

    /// <summary>
    /// Retrieves a user account from the repository based on the unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>The user account, or null if not found.</returns>
    public Task<UserAccount?> GetUserById(Guid id);

    /// <summary>
    /// Updates the password of the user account with the specified unique identifier.
    /// </summary>
    /// <param name="newPassword">The new password.</param>
    /// <param name="userId">The unique identifier of the user.</param>
    public Task UpdatePassword(string newPassword, Guid userId);

    /// <summary>
    /// Updates the email address of the user account with the specified unique identifier.
    /// </summary>
    /// <param name="newEmail">The new email address.</param>
    /// <param name="userId">The unique identifier of the user.</param>
    public Task UpdateEmail(string newEmail, Guid userId);

    /// <summary>
    /// Updates the phone number of the user account with the specified unique identifier.
    /// </summary>
    /// <param name="newPhoneNumber">The new phone number.</param>
    /// <param name="userId">The unique identifier of the user.</param>
    public Task UpdatePhoneNumber(string newPhoneNumber, Guid userId);

    public Task UpdateName(Guid userId, string? firstName, string? lastName);
}