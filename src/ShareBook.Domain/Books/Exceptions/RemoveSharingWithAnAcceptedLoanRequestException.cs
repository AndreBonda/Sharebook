using ShareBook.Domain.Shared.Exceptions;

namespace ShareBook.Domain.Books.Exceptions;

public class RemoveSharingWithAnAcceptedLoanRequestException : BadRequestException
{
    public RemoveSharingWithAnAcceptedLoanRequestException(string message) : base(message)
    {
    }

    public RemoveSharingWithAnAcceptedLoanRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }
}