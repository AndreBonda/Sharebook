using MediatR;
using Microsoft.Extensions.Logging;
using ShareBook.Domain.Books;

namespace ShareBook.Application.Books.CreateBook;

public class CreateBookHandler : IRequestHandler<CreateBookCmd>
{
    private readonly IBookRepository _repo;

    public CreateBookHandler(IBookRepository repo)
    {
        _repo = repo;
    }
    
    public async Task Handle(CreateBookCmd request, CancellationToken cancellationToken)
    {
        var book = Book.New(
            request.Id,
            request.CurrentUser,
            request.Title,
            request.Author,
            request.Pages,
            request.SharedByOwner,
            request.Labels
        );
        await _repo.AddAsync(book);
        await _repo.SaveAsync();
    }
}