namespace StoreApi.Models.Exceptions;

public sealed class BadRequestException(
    string message
) : Exception(message)
{

}