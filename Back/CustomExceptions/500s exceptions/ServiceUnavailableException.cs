namespace CustomExceptions._500s_exceptions;

public class ServiceUnavailableException : Exception, ICustomException
{
    public int StatusCode { get; }
    
    public ServiceUnavailableException(string message, int statusCode = 503)
        : base(message)
    {
        StatusCode = statusCode;
    }

    
}