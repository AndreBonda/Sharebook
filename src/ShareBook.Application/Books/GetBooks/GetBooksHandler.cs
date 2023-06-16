using MediatR;
using ShareBook.Application.Books.ViewModels;

namespace ShareBook.Application.Books;

public class GetBooksHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookVM>>
{
    private readonly IBookQueries _bookQueries;

    public GetBooksHandler(IBookQueries bookQueries)
    {
        _bookQueries = bookQueries;
    }

    public async Task<IEnumerable<BookVM>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        return await _bookQueries.GetBooksByTitleAsync(request.Title);
    }
}