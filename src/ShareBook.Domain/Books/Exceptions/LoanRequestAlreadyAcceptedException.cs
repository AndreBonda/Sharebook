using ShareBook.Domain.Shared.Exceptions;

namespace ShareBook.Domain.Books.Exceptions;

public class LoanRequestAlreadyAcceptedException : BadRequestException
{
    public LoanRequestAlreadyAcceptedException()
    {
    }

    public LoanRequestAlreadyAcceptedException(string message) : base(message)
    {
    }

    public LoanRequestAlreadyAcceptedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}