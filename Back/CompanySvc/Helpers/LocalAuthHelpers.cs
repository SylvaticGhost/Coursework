using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GlobalHelpers;
using Microsoft.IdentityModel.Tokens;

namespace CompanySvc.Helpers;

/// <summary>
/// This class provides helper methods for local authentication.
/// It inherits from the GlobalAuthHelpers class.
/// </summary>
public class LocalAuthHelpers(IConfiguration configuration) : GlobalAuthHelpers
{
    /// <summary>
    /// Generates a JWT token for a given company ID.
    /// </summary>
    /// <param name="companyId">The ID of the company for which to generate the token.</param>
    /// <returns>A JWT token as a string.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the token key is not found in the configuration.</exception>
    public string GenerateJwtToken(Guid companyId)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.PrimarySid, companyId.ToString())
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