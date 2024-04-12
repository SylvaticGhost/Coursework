namespace CustomExceptions._400s;

public class BadRequestException(string message) : Exception(message)
{
    public int StatusCode { get; } = 400;
}