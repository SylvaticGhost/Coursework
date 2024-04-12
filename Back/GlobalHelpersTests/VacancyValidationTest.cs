using GlobalHelpers;
using GlobalHelpers.Models;
using GlobalModels.Vacancy;

[TestFixture]
public class VacancyValidationTests
{
    [Test]
    public void ValidateVacancyInputFields_AllFieldsValid_ReturnsNoErrors()
    {
        var vacancy = new VacancyToAddDto("Title", "Description", "Salary", "Experience", "Specialization");
        var result = VacancyValidation.ValidateVacancyInputFields(vacancy);
        Assert.IsTrue(result.IsValid);
    }

    [Test]
    public void ValidateVacancyInputFields_TitleMissing_ReturnsTitleError()
    {
        var vacancy = new VacancyToAddDto("", "Description", "Salary", "Experience", "Specialization");
        var result = VacancyValidation.ValidateVacancyInputFields(vacancy);
        Assert.IsTrue(result.ContainsError("Title is required"));
    }

    [Test]
    public void ValidateVacancyInputFields_DescriptionMissing_ReturnsDescriptionError()
    {
        var vacancy = new VacancyToAddDto("Title", "", "Salary", "Experience", "Specialization");
        var result = VacancyValidation.ValidateVacancyInputFields(vacancy);
        Assert.IsTrue(result.ContainsError("Description is required"));
    }

    [Test]
    public void ValidateVacancyInputFields_SalaryMissing_ReturnsSalaryError()
    {
        var vacancy = new VacancyToAddDto("Title", "Description", "", "Experience", "Specialization");
        var result = VacancyValidation.ValidateVacancyInputFields(vacancy);
        Assert.IsTrue(result.ContainsError("Salary is required"));
    }

    [Test]
    public void ValidateVacancyInputFields_ExperienceMissing_ReturnsExperienceError()
    {
        var vacancy = new VacancyToAddDto("Title", "Description", "Salary", "", "Specialization");
        var result = VacancyValidation.ValidateVacancyInputFields(vacancy);
        Assert.IsTrue(result.ContainsError("Experience is required"));
    }

    [Test]
    public void ValidateVacancyInputFields_SpecializationMissing_ReturnsSpecializationError()
    {
        var vacancy = new VacancyToAddDto("Title", "Description", "Salary", "Experience", "");
        var result = VacancyValidation.ValidateVacancyInputFields(vacancy);
        Assert.IsTrue(result.ContainsError("Specialization is required"));
    }
}