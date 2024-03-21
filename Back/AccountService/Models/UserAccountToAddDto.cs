using System.ComponentModel.DataAnnotations;
using GlobalHelpers;
using GlobalHelpers.Models;

namespace AccountService.Models;

public record UserAccountToAddDto(
    [Required] string FirstName,
    string? LastName,
    [Required] [EmailAddress] string Email,
    [Required] string PhoneNumber,
    [Required] DateTime DateOfBirth,
    [Required] string Password
)
{
    public ValidationResults Validate()
    {
        ValidationResults validationResult = new ValidationResults();
        
        if(!Validation.ValidateEmail(Email))
            validationResult.AddError("Email");
        
        if(!Validation.ValidatePhoneNumber(PhoneNumber))
            validationResult.AddError("PhoneNumber");
        
        return validationResult;
    }
}