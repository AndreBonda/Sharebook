using MediatR;
using Microsoft.Extensions.Logging;
using ShareBook.Domain.Books;
using ShareBook.Domain.Shared.Exceptions;

namespace ShareBook.Application.Books.CreateBook;

public class RefuseLoanRequestHandler : IRequestHandler<RefuseLoanRequestCmd>
{
    private readonly IBookRepository _repository;

    public RefuseLoanRequestHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(RefuseLoanRequestCmd request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetByIdAsync(request.BookId);

        if(book is null)
            throw new NotFoundException();

        book.RefuseLoanRequest(request.RequestingUser);
        await _repository.SaveAsync();
    }
}