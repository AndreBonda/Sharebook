using ShareBook.Domain.Shared.Exceptions;

namespace ShareBook.Domain.Books.Exceptions;

public class UpdateBookUserNotOwnerException : ForbiddenException
{
    public UpdateBookUserNotOwnerException()
    {
    }

    public UpdateBookUserNotOwnerException(string message) : base(message)
    {
    }

    public UpdateBookUserNotOwnerException(string message, Exception innerException) : base(message, innerException)
    {
    }
}