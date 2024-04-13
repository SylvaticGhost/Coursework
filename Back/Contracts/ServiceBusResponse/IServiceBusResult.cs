namespace Contracts;

public interface IServiceBusResult<T>
{
    public T? Result { get; set; }
    
    public bool IsSuccess { get; set; }
    
    public string ErrorMessage { get; set; }
}