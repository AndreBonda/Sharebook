using MediatR;
using Microsoft.Extensions.Logging;
using ShareBook.Domain.Books;
using ShareBook.Domain.Shared.Exceptions;

namespace ShareBook.Application.Books.CreateBook;

public class CreateLoanRequestHandler : IRequestHandler<CreateLoanRequestCmd>
{
    private readonly IBookRepository _repository;

    public CreateLoanRequestHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateLoanRequestCmd request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetByIdAsync(request.BookId);

        if(book is null)
            throw new NotFoundException();

        book.RequestNewLoan(request.RequestingUser);
        await _repository.SaveAsync();
    }
}