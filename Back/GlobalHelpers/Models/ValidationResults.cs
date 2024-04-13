using System.Text;

namespace GlobalHelpers.Models;

public class ValidationResults
{
    public bool IsValid => Errors.Count <= 0;

    private List<string> Errors { get; } = new();
    
    public void AddError(string error)
    {
        Errors.Add(error);
    }


    public override string ToString()
    {
        StringBuilder stringBuilder = new();
        
        foreach (string error in Errors)
        {
            stringBuilder.AppendLine(error);
        }
        
        return stringBuilder.ToString();
    }
    
    
    public bool ContainsError(string error) => Errors.Contains(error);
}