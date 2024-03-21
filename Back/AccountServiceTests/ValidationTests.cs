using AccountService.Helpers;
using AccountService.Models.HelpersModels;
using Xunit;
using Assert = Xunit.Assert;

namespace AccountServiceTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Fact]
    public void CreatePasswordHash_ReturnsHashedPasswords_WhenPasswordIsValid()
    {
        // Arrange
        var password = "ValidPassword";

        // Act
        var result = AuthHelpers.CreatePasswordHash(password);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<HashedPasswords>(result);
        Assert.Equal(64, result.PasswordHash.Length);
        Assert.Equal(128, result.PasswordSalt.Length);
    }

    [Fact]
    public void CreatePasswordHash_ThrowsArgumentNullException_WhenPasswordIsNull()
    {
        // Arrange
        string password = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => AuthHelpers.CreatePasswordHash(password));
    }
}