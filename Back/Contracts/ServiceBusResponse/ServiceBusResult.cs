namespace Contracts;

public record ServiceBusResult<T> : IServiceBusResult<T>
{
    public T? Result { get; set; }
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; }
    
    public bool Empty => Result == null;

    public ServiceBusResult()
    {
        ErrorMessage ??= string.Empty;
    }
}