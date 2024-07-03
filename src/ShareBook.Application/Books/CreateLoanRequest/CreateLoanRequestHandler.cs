using MediatR;
using ShareBook.Domain.Books;
using ShareBook.Domain.Shared.Exceptions;

namespace ShareBook.Application.Books;

public class CreateLoanRequestHandler(IBookRepository repository) : IRequestHandler<CreateLoanRequestCmd>
{
    public async Task Handle(CreateLoanRequestCmd request, CancellationToken cancellationToken)
    {
        var book = await repository.GetByIdAsync(request.BookId);

        if(book is null)
            throw new NotFoundException();

        book.RequestNewLoan(request.UserId);

        await repository.UpdateLoanRequests(book);
        await repository.SaveAsync();
    }
}