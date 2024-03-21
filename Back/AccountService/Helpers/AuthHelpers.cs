using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AccountService.Models;
using AccountService.Models.HelpersModels;
using Microsoft.IdentityModel.Tokens;

namespace AccountService.Helpers
{
    /// <summary>
    /// This class provides helper methods for password hashing and verification.
    /// </summary>
    public class AuthHelpers(IConfiguration configuration)
    {
        /// <summary>
        /// Creates a password hash and salt using HMACSHA512.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="passwordHash">The resulting password hash.</param>
        /// <param name="passwordSalt">The resulting password salt.</param>
        /// <exception cref="ArgumentNullException">Thrown when password is null.</exception>
        /// <exception cref="ArgumentException">Thrown when password is empty or whitespace.</exception>
        public static HashedPasswords CreatePasswordHash(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            using var hmac = new HMACSHA512();
            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return new HashedPasswords(passwordHash, passwordSalt);
        }


        /// <summary>
        /// Verifies a password against a stored hash and salt.
        /// </summary>
        /// <param name="password">The password to verify.</param>
        /// <param name="storedHash">The stored password hash.</param>
        /// <param name="storedSalt">The stored password salt.</param>
        /// <returns>True if the password matches the stored hash and salt, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when password is null.</exception>
        /// <exception cref="ArgumentException">Thrown when password is empty or whitespace, or when storedHash or storedSalt have invalid lengths.</exception>
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            if (storedHash.Length != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(storedHash));
            if (storedSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).",
                    nameof(storedSalt));

            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (var i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != storedHash[i]) return false;
            }

            return true;
        }


        /// <summary>
        /// Generates a JWT (JSON Web Token) for a given user.
        /// </summary>
        /// <param name="user" cref="UserAccount">The user for whom the JWT is to be generated.</param>
        /// <returns>A JWT as a string.</returns>
        /// <remarks>
        /// The JWT includes claims for the user's ID, name, and email.
        /// The JWT is signed using a secret key from the configuration, and it's set to expire after 1 day.
        /// </remarks>
        /// <exception cref="InvalidOperationException">Thrown when the token key is not found in the configuration.</exception>
        public string GenerateJwtToken(UserAccount user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var value = configuration.GetSection("Jwt:Key").Value;
            
            if (value == null) 
                throw new InvalidOperationException("Token key not found");
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
                Issuer = configuration.GetSection("Jwt:Issuer").Value,
                Audience = configuration.GetSection("Jwt:Audience").Value
            };
                
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}