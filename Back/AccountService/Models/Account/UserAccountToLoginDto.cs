namespace AccountService.Models;

public struct UserAccountToLoginDto
{
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string Password { get; set; }
}