using ShareBook.Domain.Shared.Exceptions;

namespace ShareBook.Domain.Books.Exceptions;

public class BookOwnerCannotMakeALoanRequest : BadRequestException
{
    public BookOwnerCannotMakeALoanRequest()
    {
    }

    public BookOwnerCannotMakeALoanRequest(string message) : base(message)
    {
    }

    public BookOwnerCannotMakeALoanRequest(string message, Exception innerException) : base(message, innerException)
    {
    }
}