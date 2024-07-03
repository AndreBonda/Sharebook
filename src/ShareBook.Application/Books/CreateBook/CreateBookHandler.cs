using MediatR;
using ShareBook.Domain.Books;

namespace ShareBook.Application.Books;

public class CreateBookHandler(IBookRepository repository) : IRequestHandler<CreateBookCmd>
{
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
        await repository.AddAsync(book);
        await repository.SaveAsync();
    }
}