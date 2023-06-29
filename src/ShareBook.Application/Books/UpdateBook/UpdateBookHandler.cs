using MediatR;
using ShareBook.Domain.Books;
using ShareBook.Domain.Shared.Exceptions;

namespace ShareBook.Application.Books;

public class UpdateBookHandler : IRequestHandler<UpdateBookCmd>
{
    private readonly IBookRepository _repo;

    public UpdateBookHandler(IBookRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(UpdateBookCmd request, CancellationToken cancellationToken)
    {
        var book = await _repo.GetByIdAsync(request.Id);

        if(book is null)
            throw new NotFoundException(nameof(book), request.Id);

        // TODO: gestire concorrenza
        book.Update(
            Guid.Empty,
            request.Title,
            request.Author,
            request.Pages,
            request.SharedByOwner,
            request.Labels
        );

       await _repo.SaveAsync();
    }
}