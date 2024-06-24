using ShareBook.Domain.Shared.Exceptions;

namespace ShareBook.Domain.Books.Exceptions;

public class LoanRequestAlreadyExistsForUserException : BadRequestException
{
    public LoanRequestAlreadyExistsForUserException(string message) : base(message)
    {
    }

    public LoanRequestAlreadyExistsForUserException(string message, Exception innerException) : base(message, innerException)
    {
    }
}