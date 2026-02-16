namespace Discounts.Domain.Exceptions;

public class NotFoundException
{
    public NotFoundException() { }
    public NotFoundException(string message) { }
    public NotFoundException(string message, Exception innerException) { }
}