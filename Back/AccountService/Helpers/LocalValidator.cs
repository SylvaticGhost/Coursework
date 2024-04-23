using System.ComponentModel.DataAnnotations;
using AccountService.Models;
using AutoMapper;
using GlobalHelpers;
using GlobalHelpers.Models;
using GlobalModels;

namespace AccountService.Helpers;

public class LocalValidator(IMapper mapper) : Validation
{
    //TODO: Rewrite to ValidationResults answer
    public static bool ValidateProfile(UserProfileToAddDto userProfile)
    {
        if(!CheckIfWord(userProfile.Country) || !CheckIfWord(userProfile.City, nullable: true))
            return false;

        return true;
    }


    public static ValidationResults ValidateContact(Contact contact)
    {
        ValidationResults validationResult = new();
        
        if (string.IsNullOrEmpty(contact.Link))
            validationResult.AddError("Link is empty");
        
        return validationResult;
    }
    
    
    public ValidationResults ValidateProfileToUpdate(UserProfileToUpdateDto userProfile)
    {
        UserProfileToAddDto userProfileToAddDto = mapper.Map<UserProfileToAddDto>(userProfile);
        
        ValidationResults validationResult = new();
        
        if(!ValidateProfile(userProfileToAddDto))
            validationResult.AddError("Profile main info is not valid");
        
        if(!CheckIfWord(userProfile.About, nullable: true))
            validationResult.AddError("About is not valid");
        
        if(!CheckIfWord(userProfile.FirstName, nullable: true))
            validationResult.AddError("First name is not valid");
        
        if(!CheckIfWord(userProfile.LastName, nullable: true))
            validationResult.AddError("Last name is not valid");
            
        return validationResult;
    }
    
}