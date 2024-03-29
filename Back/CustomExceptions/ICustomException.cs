namespace CustomExceptions;

public interface ICustomException
{
    int StatusCode { get; }
    string Message { get; }
}