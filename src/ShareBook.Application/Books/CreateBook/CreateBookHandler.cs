using MediatR;
using ShareBook.Domain.Books;

namespace ShareBook.Application.Books;

public class CreateBookHandler : IRequestHandler<CreateBookCmd>
{
    private readonly IBookRepository _repository;

    public CreateBookHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateBookCmd request, CancellationToken cancellationToken)
    {
        Book book = new(
            request.Id,
            request.UserId,
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