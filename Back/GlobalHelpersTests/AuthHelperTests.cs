using NUnit.Framework;
using System;
using GlobalHelpers;
using GlobalHelpers.Models;

namespace GlobalHelpersTests
{
    public class GlobalAuthHelpersTests
    {
        [Test]
        public void CreatePasswordHash_WithValidPassword_ReturnsHashAndSalt()
        {
            // Arrange
            var password = "validPassword";

            // Act
            var result = GlobalAuthHelpers.CreatePasswordHash(password);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(64, result.PasswordHash.Length);
            Assert.AreEqual(128, result.PasswordSalt.Length);
        }

        [Test]
        public void CreatePasswordHash_WithNullPassword_ThrowsArgumentNullException()
        {
            // Arrange
            string password = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => GlobalAuthHelpers.CreatePasswordHash(password));
        }

        [Test]
        public void CreatePasswordHash_WithEmptyPassword_ThrowsArgumentException()
        {
            // Arrange
            var password = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => GlobalAuthHelpers.CreatePasswordHash(password));
        }

        [Test]
        public void VerifyPasswordHash_WithValidPasswordAndHash_ReturnsTrue()
        {
            // Arrange
            var password = "validPassword";
            var hashedPasswords = GlobalAuthHelpers.CreatePasswordHash(password);

            // Act
            var result = GlobalAuthHelpers.VerifyPasswordHash(password, hashedPasswords.PasswordHash, hashedPasswords.PasswordSalt);

            // Assert
            Assert.True(result);
        }

        [Test]
        public void VerifyPasswordHash_WithInvalidPassword_ReturnsFalse()
        {
            // Arrange
            var password = "validPassword";
            var invalidPassword = "invalidPassword";
            var hashedPasswords = GlobalAuthHelpers.CreatePasswordHash(password);

            // Act
            var result = GlobalAuthHelpers.VerifyPasswordHash(invalidPassword, hashedPasswords.PasswordHash, hashedPasswords.PasswordSalt);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void VerifyPasswordHash_WithInvalidHashLength_ThrowsArgumentException()
        {
            // Arrange
            var password = "validPassword";
            var hashedPasswords = GlobalAuthHelpers.CreatePasswordHash(password);
            var invalidHash = new byte[63];

            // Act & Assert
            Assert.Throws<ArgumentException>(() => GlobalAuthHelpers.VerifyPasswordHash(password, invalidHash, hashedPasswords.PasswordSalt));
        }

        [Test]
        public void VerifyPasswordHash_WithInvalidSaltLength_ThrowsArgumentException()
        {
            // Arrange
            var password = "validPassword";
            var hashedPasswords = GlobalAuthHelpers.CreatePasswordHash(password);
            var invalidSalt = new byte[127];

            // Act & Assert
            Assert.Throws<ArgumentException>(() => GlobalAuthHelpers.VerifyPasswordHash(password, hashedPasswords.PasswordHash, invalidSalt));
        }
    }
}