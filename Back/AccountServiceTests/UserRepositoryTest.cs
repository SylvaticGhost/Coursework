using NUnit.Framework;
using Moq;
using AccountService.Repositories;
using AccountService.Models;
using AccountService.Data;
using Microsoft.Extensions.Configuration;
using System;

[TestFixture]
public class UserRepositoryTests
{
    [Test]
    public async Task RefreshToken_ReturnsToken_WhenUserExists()
    {
        // Arrange
        var mockContext = new Mock<DataContextNpgEf>();
        var mockConfiguration = new Mock<IConfiguration>();
        var userRepository = new UserRepository(mockContext.Object, mockConfiguration.Object);
        var userId = Guid.NewGuid();

        // Act
        var result = await userRepository.RefreshToken(userId);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<string>(result);
    }

    [Test]
    public void RefreshToken_ThrowsException_WhenUserDoesNotExist()
    {
        // Arrange
        var mockContext = new Mock<DataContextNpgEf>();
        var mockConfiguration = new Mock<IConfiguration>();
        var userRepository = new UserRepository(mockContext.Object, mockConfiguration.Object);
        var userId = Guid.NewGuid();

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(async () => await userRepository.RefreshToken(userId));
    }
}