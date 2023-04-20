using MediatR;
using ShareBook.API.Books;

namespace ShareBook.Application.Books.GetBooks;

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