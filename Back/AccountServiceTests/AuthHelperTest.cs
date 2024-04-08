using AccountService.Helpers;
using AccountService.Models;
using GlobalHelpers.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace AccountServiceTests;

public class AuthHelperTest
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
    
    
    [Fact]
    public void GenerateJwtToken_ReturnsExpectedToken_WhenUserIsValid()
    {
        // Arrange
        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration.Setup(c => c.GetSection("Jwt:Key").Value).Returns("ijuhbnuyo8irbgeh8iuerbuybvuyB!ub3hb3uybvuyghvbuyvyuiyovgtgyVIU#bhbfishbnhfiuodshnfuibsncvjmsdio");
        mockConfiguration.Setup(c => c.GetSection("Jwt:Issuer").Value).Returns("SomeIssuer");
        mockConfiguration.Setup(c => c.GetSection("Jwt:Audience").Value).Returns("SomeAudience");

        var authHelpers = new AuthHelpers(mockConfiguration.Object);
        var user = new UserAccount { FirstName = "Test", Email = "test@test.com" };

        // Act
        var token = authHelpers.GenerateJwtToken(user);

        // Assert
        Assert.NotNull(token);
        Assert.IsType<string>(token);
    }

    [Fact]
    public void GenerateJwtToken_ThrowsException_WhenKeyIsMissing()
    {
        // Arrange
        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration.Setup(c => c.GetSection("Jwt:Key").Value).Returns((string)null);

        var authHelpers = new AuthHelpers(mockConfiguration.Object);
        var user = new UserAccount { FirstName = "Test", Email = "test@test.com" };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => authHelpers.GenerateJwtToken(user));
    }
}