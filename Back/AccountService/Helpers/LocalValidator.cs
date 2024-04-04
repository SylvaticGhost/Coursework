using AccountService.Models;
using GlobalHelpers;

namespace AccountService.Helpers;

public class LocalValidator : Validation
{
    public static bool ValidateProfile(UserProfileToAddDto userProfile)
    {
        if(!CheckIfWord(userProfile.Country) || !CheckIfWord(userProfile.City, nullable: true))
            return false;

        return true;
    }
}