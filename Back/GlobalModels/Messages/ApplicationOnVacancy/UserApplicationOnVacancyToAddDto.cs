namespace GlobalModels.Messages;

public record UserApplicationOnVacancyToAddDto
{
    public Guid VacancyId { get; init; }
    public ShortResume ShortResume { get; init; }

    public UserApplicationOnVacancyToAddDto(Guid vacancyId, ShortResume? shortResume)
    {
        if (vacancyId == Guid.Empty)
            throw new ArgumentException("Vacancy id is empty", nameof(vacancyId));
        
        ArgumentNullException.ThrowIfNull(shortResume);
        
        VacancyId = vacancyId;
        ShortResume = shortResume;
    }
}