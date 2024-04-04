using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GlobalHelpers;

/// <summary>
/// Contains methods for validating various types of data.
/// </summary>
public partial class Validation
{
    /// <summary>
    /// Validates an email address.
    /// </summary>
    /// <param name="email">The email address to validate.</param>
    /// <returns>True if the email address is valid, false otherwise.</returns>
    public static bool ValidateEmail(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }

    /// <summary>
    /// Validates a phone number.
    /// </summary>
    /// <param name="phoneNumber">The phone number to validate.</param>
    /// <returns>True if the phone number is valid, false otherwise.</returns>
    public static bool ValidatePhoneNumber(string phoneNumber)
    {
        if(phoneNumber.Length is < 5 or > 15)
            return false;

        Regex regex = RegexForPhoneCheck();
        return regex.IsMatch(phoneNumber);
    }

    /// <summary>
    /// Checks if a string is a word.
    /// </summary>
    /// <param name="input">The string to check.</param>
    /// <param name="nullable">Whether the string can be null or whitespace.</param>
    /// <returns>True if the string is a word, false otherwise.</returns>
    public static bool CheckIfWord(string? input, bool nullable = false)
    {
        if (string.IsNullOrWhiteSpace(input) && !nullable)
            return false;

        if(nullable && string.IsNullOrWhiteSpace(input))
            return true;
        
        return input!.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
    }

    /// <summary>
    /// Generates a regex for phone number validation.
    /// </summary>
    /// <returns>A regex for phone number validation.</returns>
    [GeneratedRegex(@"^\+?\d+$")]
    private static partial Regex RegexForPhoneCheck();
}