using ShareBook.Domain.Shared.Exceptions;

namespace ShareBook.Domain.Books.Exceptions;

public class UserIsNotBookOwnerException : ForbiddenException
{
    public UserIsNotBookOwnerException()
    {
    }

    public UserIsNotBookOwnerException(string message) : base(message)
    {
    }

    public UserIsNotBookOwnerException(string message, Exception innerException) : base(message, innerException)
    {
    }
}