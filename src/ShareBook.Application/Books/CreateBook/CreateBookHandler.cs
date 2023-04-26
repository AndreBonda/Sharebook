using MediatR;
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
            request.Owner,
            request.Title,
            request.Author,
            request.Pages,
            request.Labels
        );
        await _repo.AddAsync(book);
        await _repo.SaveAsync();
    }
}