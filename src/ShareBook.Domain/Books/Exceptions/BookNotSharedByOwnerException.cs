using ShareBook.Domain.Shared.Exceptions;

namespace ShareBook.Domain.Books.Exceptions;

public class BookNotSharedByOwnerException : BadRequestException
{
    public BookNotSharedByOwnerException()
    {
    }

    public BookNotSharedByOwnerException(string message) : base(message)
    {
    }

    public BookNotSharedByOwnerException(string message, Exception innerException) : base(message, innerException)
    {
    }
}