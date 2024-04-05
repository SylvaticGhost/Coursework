using AccountService.Helpers;
using AccountService.Models;
using GlobalModels;

namespace AccountServiceTests.HelpersTest;

[TestFixture]
public class LocalValidatorTests
{
    [Test]
    public void ValidateProfile_ReturnsTrue_WhenProfileIsValid()
    {
        // Arrange
        var validProfile = 
            new UserProfileToAddDto(City: "New York", Country: "USA", Contacts: null, About: "desc", Avatar: null);

        // Act
        var result = LocalValidator.ValidateProfile(validProfile);

        // Assert
        Assert.IsTrue(result);
    }
    
    [Test]
    public void ValidateProfile_ReturnsUndef_WhenProfileIsValid()
    {
        var contact = new Contact()
            { Link = "e", TypeOfContact = TypeOfContacts.Discord, DisplayName = "e", IsVerified = true };
        // Arrange
        var validProfile = 
            new UserProfileToAddDto(City: "e", Country: "e", Contacts: [contact], About: "desc", Avatar: null);

        // Act
        var result = LocalValidator.ValidateProfile(validProfile);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void ValidateProfile_ReturnsFalse_WhenCountryIsNotAWord()
    {
        // Arrange
        var invalidProfile = 
            new UserProfileToAddDto(City: "New York", Country: "123", Contacts: null, About: "desc", Avatar: null);

        // Act
        var result = LocalValidator.ValidateProfile(invalidProfile);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void ValidateProfile_ReturnsFalse_WhenCityIsNotAWord()
    {
        // Arrange
        var invalidProfile = 
            new UserProfileToAddDto(City: "123", Country: "USA", Contacts: null, About: "desc", Avatar: null);

        // Act
        var result = LocalValidator.ValidateProfile(invalidProfile);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void ValidateProfile_ReturnsTrue_WhenCityIsNullAndCountryIsValid()
    {
        // Arrange
        var validProfile = 
            new UserProfileToAddDto(City: null, Country: "USA", Contacts: null, About: "desc", Avatar: null);

        // Act
        var result = LocalValidator.ValidateProfile(validProfile);

        // Assert
        Assert.IsTrue(result);
    }
}