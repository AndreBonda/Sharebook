using MediatR;
using ShareBook.Domain.Books;
using ShareBook.Domain.Shared.Exceptions;

namespace ShareBook.Application.Books;

public class CreateLoanRequestHandler(IBookRepository bookRepository) : IRequestHandler<CreateLoanRequestCmd>
{
    public async Task Handle(CreateLoanRequestCmd request, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetByIdAsync(request.BookId);

        if(book is null)
            throw new NotFoundException($"Book not found. (ID {request.BookId})");

        book.RequestNewLoan(request.UserId);

        await bookRepository.Update(book);
    }
}