using MediatR;
using Microsoft.Extensions.Logging;
using ShareBook.Domain.Books;
using ShareBook.Domain.Shared.Exceptions;
using ShareBook.Domain.Shippings;

namespace ShareBook.Application.Books;

public class AcceptLoanRequestHandler : IRequestHandler<AcceptLoanRequestCmd>
{
    private readonly IBookRepository _bookRepository;

    public AcceptLoanRequestHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task Handle(AcceptLoanRequestCmd request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId);

        if(book is null)
            throw new NotFoundException("Book not found");

        book.AcceptLoanRequest(request.BookOwner); 

        await _bookRepository.SaveAsync();
    }
}