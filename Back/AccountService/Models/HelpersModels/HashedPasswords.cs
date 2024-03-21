namespace AccountService.Models.HelpersModels;

public record HashedPasswords(byte[] PasswordHash, byte[] PasswordSalt);