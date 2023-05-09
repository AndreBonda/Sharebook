using MediatR;
using ShareBook.Domain.Books;

namespace ShareBook.Application.Books.CreateBook;

public class CreateBookHandler : IRequestHandler<CreateBookCmd>
{
    private readonly IBookRepository _repository;

    public CreateBookHandler(IBookRepository repository)
    {
        _repository = repository;
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
        await _repository.AddAsync(book);
        await _repository.SaveAsync();
    }
}