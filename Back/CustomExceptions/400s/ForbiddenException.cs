namespace CustomExceptions._400s;

public class ForbiddenException : Exception
{
    public int StatusCode { get; init; }
    
    public ForbiddenException(string message, int statusCode = 403)
        : base(message)
    {
        StatusCode = statusCode;
    }
}