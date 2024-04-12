namespace GlobalModels.Vacancy;

public interface IVacancyInputFields
{
    public string Title { get; }
    public string Description{ get; }
    public string Salary{ get; }
    public string Experience{ get; }
    public string Specialization{ get; }
}