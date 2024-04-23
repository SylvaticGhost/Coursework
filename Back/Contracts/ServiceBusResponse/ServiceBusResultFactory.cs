﻿namespace Contracts;

public class ServiceBusResultFactory
{
    public static IServiceBusResult<T> SuccessResult<T>(T data) => new ServiceBusResult<T> { Result = data, IsSuccess = true };
    
    public static IServiceBusResult<T> FailResult<T>(string errorMessage) =>
        new ServiceBusResult<T> { ErrorMessage = string.IsNullOrEmpty(errorMessage) ? "undefined error" : errorMessage, IsSuccess = false };
}