using MediatR;
using ShareBook.Domain.Books;

namespace ShareBook.Application.Books.CreateBook;

public class CreateBookHandler : IRequestHandler<CreateBookCmd, Guid>
{
    private readonly IBookRepository _repo;

    public CreateBookHandler(IBookRepository repo)
    {
        _repo = repo;
    }
    
    public async Task<Guid> Handle(CreateBookCmd request, CancellationToken cancellationToken)
    {
        var book = Book.New(
            request.Owner,
            request.Title,
            request.Author,
            request.Pages,
            request.Labels
        );
        await _repo.AddAsync(book);
        await _repo.SaveAsync();
        return book.Id;
    }
}