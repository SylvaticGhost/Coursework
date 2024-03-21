using System.ComponentModel.DataAnnotations;

namespace AccountService.Models;

public class UserAccount
{
    [Key]
    [Required]
    public Guid Id { get;private set; }
    
    [Required]
    [Length(2, 30)]
    public string FirstName { get; set; }
    
    [Length(2, 30)]
    public string? LastName { get; set; }
    
    [Required]
    [EmailAddress]
    [Length(3, 50)]
    public string Email { get; set; }
    
    [Required]
    [Length(5, 15)]
    public string PhoneNumber { get; set; }
    
    [Required]
    public DateTime DateOfBirth { get; set; }
    
    [Required]
    public byte[] PasswordHash { get; set; }
    
    [Required]
    public byte[] PasswordSalt { get; set; }
    
    [Required]
    public DateTime CreatedAt { get;private set; }

    
    public UserAccount()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        
        Init();
    }


    private void Init()
    {
        FirstName ??= string.Empty;
        LastName ??= string.Empty;
        Email ??= string.Empty;
        PhoneNumber ??= string.Empty;
        
        PasswordHash = Array.Empty<byte>();
        PasswordSalt = Array.Empty<byte>();
    }
}