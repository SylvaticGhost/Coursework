namespace GlobalHelpers.Models;

public class ValidationResults
{
    public bool Result => Errors.Count <= 0;

    private List<string> Errors { get; set; } = new();
    
    public void AddError(string error)
    {
        Errors.Add(error);
    }
}