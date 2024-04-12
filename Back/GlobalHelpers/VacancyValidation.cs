using GlobalHelpers.Models;
using GlobalModels.Vacancy;

namespace GlobalHelpers;

public class VacancyValidation : Validation
{
    public static ValidationResults ValidateVacancyInputFields(IVacancyInputFields vacancy)
    {
        ValidationResults validationResults = new();
        
        if (string.IsNullOrWhiteSpace(vacancy.Title))
        {
            validationResults.AddError("Title is required");
        }
        if (string.IsNullOrWhiteSpace(vacancy.Description))
        {
            validationResults.AddError("Description is required");
        }
        if (string.IsNullOrWhiteSpace(vacancy.Salary))
        {
            validationResults.AddError("Salary is required");
        }
        if (string.IsNullOrWhiteSpace(vacancy.Experience))
        {
            validationResults.AddError("Experience is required");
        }
        if (string.IsNullOrWhiteSpace(vacancy.Specialization))
        {
            validationResults.AddError("Specialization is required");
        }

        return validationResults;
    }
}