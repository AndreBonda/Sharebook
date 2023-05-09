using ShareBook.Domain.Shared.Exceptions;

namespace ShareBook.Domain.Books.Exceptions;

public class NonExistingLoanRequestException : BadRequestException
{
    public NonExistingLoanRequestException()
    {
    }

    public NonExistingLoanRequestException(string message) : base(message)
    {
    }

    public NonExistingLoanRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }
}