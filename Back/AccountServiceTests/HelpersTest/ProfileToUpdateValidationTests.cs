using NUnit.Framework;
using Moq;
using AccountService.Helpers;
using AccountService.Models;
using AutoMapper;
using GlobalHelpers.Models;
using GlobalModels;

namespace AccountServiceTests.HelpersTest
{
    [TestFixture]
    public class ProfileToUpdateValidationTests
    {
        private Mock<IMapper> _mockMapper;
        private LocalValidator _localValidator;

        /// <summary>
        /// Random contact object
        /// </summary>
        private Contact _contact;

        [SetUp]
        public void Setup()
        {
            _mockMapper = new Mock<IMapper>();
            _localValidator = new LocalValidator(_mockMapper.Object);
            _contact= new  Contact
            {
                Link = "https://www.google.com",
                TypeOfContact = TypeOfContacts.Email,
                DisplayName = "John Doe",
                IsVerified = true
            };
        }

        [Test]
        public void ValidateProfileToUpdate_ReturnsNoErrors_WhenProfileIsValid()
        {
            // Arrange
            var userProfile = new UserProfileToUpdateDto("John", "Doe", new List<Contact> { _contact }, "USA", null, "last name", "last");
            var userProfileToAddDto = new UserProfileToAddDto("John", "Doe", new List<Contact> { _contact }, "a", null);
            _mockMapper.Setup(m => m.Map<UserProfileToAddDto>(It.IsAny<UserProfileToUpdateDto>())).Returns(userProfileToAddDto);

            // Act
            var result = _localValidator.ValidateProfileToUpdate(userProfile);

            // Assert
            Assert.IsTrue(result.ErrorCount == 0);
        }

        [Test]
        public void ValidateProfileToUpdate_ReturnsError_WhenFirstNameIsInvalid()
        {
            // Arrange
            var userProfile = new UserProfileToUpdateDto("123", "Doe", new List<Contact> { _contact }, "USA", null, "last name", "last");
            var userProfileToAddDto = new UserProfileToAddDto("123", "Doe", new List<Contact> { _contact }, "a", null);
            _mockMapper.Setup(m => m.Map<UserProfileToAddDto>(It.IsAny<UserProfileToUpdateDto>())).Returns(userProfileToAddDto);

            // Act
            var result = _localValidator.ValidateProfileToUpdate(userProfile);

            // Assert
            Assert.IsTrue(result.ErrorCount > 0);
            //Assert.IsTrue(result.ContainsError("First name is not valid"));
        }

        [Test]
        public void ValidateProfileToUpdate_ReturnsError_WhenLastNameIsInvalid()
        {
            // Arrange
            var userProfile = new UserProfileToUpdateDto("John", "123", new List<Contact> { _contact }, "USA", null, "last name", "last");
            var userProfileToAddDto = new UserProfileToAddDto("John", "123", new List<Contact> { _contact }, "a", null);
            _mockMapper.Setup(m => m.Map<UserProfileToAddDto>(It.IsAny<UserProfileToUpdateDto>())).Returns(userProfileToAddDto);

            // Act
            var result = _localValidator.ValidateProfileToUpdate(userProfile);

            // Assert
            Assert.IsTrue(result.ErrorCount > 0);
            //Assert.IsTrue(result.ContainsError("Last name is not valid"));
        }
    }
}