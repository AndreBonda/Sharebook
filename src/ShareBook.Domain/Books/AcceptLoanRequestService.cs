using ShareBook.Domain.Shared.Exceptions;
using ShareBook.Domain.Shippings;

namespace ShareBook.Domain.Books;

public class AcceptLoanRequestService
{
    public AcceptLoanRequestService()
    {}

    public Shipping AcceptLoanRequest(Book book, string bookOwner)
    {
        if(book is null)
            throw new ArgumentNullException(nameof(book));

        book.AcceptLoanRequest(bookOwner);
        return Shipping.New(Guid.NewGuid(), book.Id);
    }
}