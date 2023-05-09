using ShareBook.Domain.Shared.Exceptions;

namespace ShareBook.Domain.Books.Exceptions;

public class RemoveSharingWithCurrentLoanRequestException : BadRequestException
{
    public RemoveSharingWithCurrentLoanRequestException()
    {
    }

    public RemoveSharingWithCurrentLoanRequestException(string message) : base(message)
    {
    }

    public RemoveSharingWithCurrentLoanRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }
}