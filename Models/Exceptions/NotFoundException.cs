namespace StoreApi.Models.Exceptions;

public sealed class NotFoundException(string message) 
    : Exception(message)
{
}