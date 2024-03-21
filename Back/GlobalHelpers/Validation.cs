using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GlobalHelpers;

public static partial class Validation
{
    public static bool ValidateEmail(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }
    
    
    public static bool ValidatePhoneNumber(string phoneNumber)
    {
        if(phoneNumber.Length is < 5 or > 15)
            return false;
        
        Regex regex = RegexForPhoneCheck();
        return regex.IsMatch(phoneNumber);
    }

    
    [GeneratedRegex(@"^\+?\d+$")]
    private static partial Regex RegexForPhoneCheck();
}