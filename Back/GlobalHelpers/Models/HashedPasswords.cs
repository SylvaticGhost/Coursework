namespace GlobalHelpers.Models;

public record HashedPasswords(byte[] PasswordHash, byte[] PasswordSalt);