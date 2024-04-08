using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AccountService.Models;
using GlobalHelpers;
using GlobalHelpers.Models;
using Microsoft.IdentityModel.Tokens;

namespace AccountService.Helpers
{
    /// <summary>
    /// This class provides helper methods for password hashing and verification.
    /// </summary>
    public class AuthHelpers(IConfiguration configuration) : GlobalAuthHelpers
    {
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