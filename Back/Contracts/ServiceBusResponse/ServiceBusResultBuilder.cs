namespace Contracts;

public class ServiceBusResultBuilder
{
    public static IServiceBusResult<T> Success<T>(T data) => new ServiceBusResult<T> { Result = data, IsSuccess = true };
    
    public static IServiceBusResult<T> Fail<T>(string errorMessage) =>
        new ServiceBusResult<T> { ErrorMessage = string.IsNullOrEmpty(errorMessage) ? "undefined error" : errorMessage, IsSuccess = false };
}