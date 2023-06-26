using ShareBook.Domain.Shared.Exceptions;

namespace ShareBook.Domain.Users.Exceptions;

public class BadCredentialException : BadRequestException
{
    public BadCredentialException() : base()
    {
    }

    public BadCredentialException(string message) : base(message)
    {
    }

    public BadCredentialException(string message, Exception innerException) : base(message, innerException)
    {
    }
}