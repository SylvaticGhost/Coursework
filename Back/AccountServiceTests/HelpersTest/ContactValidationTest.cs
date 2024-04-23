using AccountService.Helpers;
using GlobalModels;

namespace AccountServiceTests.HelpersTest
{
    [TestFixture]
    public class ContactValidationTest
    {
        [Test]
        public void ValidateContact_ReturnsNoErrors_WhenLinkIsNotEmpty()
        {
            // Arrange
            var contact = new Contact { Link = "https://example.com" };

            // Act
            var result = LocalValidator.ValidateContact(contact);

            // Assert
            Assert.IsTrue(result.ErrorCount == 0);
        }

        [Test]
        public void ValidateContact_ReturnsError_WhenLinkIsEmpty()
        {
            // Arrange
            var contact = new Contact { Link = string.Empty };

            // Act
            var result = LocalValidator.ValidateContact(contact);

            // Assert
            Assert.IsTrue(result.ErrorCount > 0);
            Assert.IsTrue(result.ContainsError("Link is empty"));
        }

        [Test]
        public void ValidateContact_ReturnsError_WhenLinkIsNull()
        {
            // Arrange
            var contact = new Contact { Link = null };

            // Act
            var result = LocalValidator.ValidateContact(contact);

            // Assert
            Assert.IsTrue(result.ErrorCount > 0);
            Assert.IsTrue(result.ContainsError("Link is empty"));
        }
    }
}