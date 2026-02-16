namespace Discounts.Domain.Exceptions;

public class ForbiddenException
{
    public ForbiddenException() { }
    public ForbiddenException(string message) { }
    public ForbiddenException(string message, Exception innerException) { }
}