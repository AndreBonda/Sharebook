using ShareBook.Domain.Shared.Exceptions;

namespace ShareBook.Domain.Books.Exceptions;

public class LoanRequestAlreadyExistsException : BadRequestException
{
    public LoanRequestAlreadyExistsException()
    {
    }

    public LoanRequestAlreadyExistsException(string message) : base(message)
    {
    }

    public LoanRequestAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}