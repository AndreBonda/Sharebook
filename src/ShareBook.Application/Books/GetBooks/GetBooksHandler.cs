using MediatR;

namespace ShareBook.Application.Books.GetBooks;

public class GetBooksHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookVM>>
{
    private readonly BookQueries _bookQueries;

    public GetBooksHandler(BookQueries bookQueries)
    {
        _bookQueries = bookQueries;
    }

    public async Task<IEnumerable<BookVM>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        return await _bookQueries.GetBooksByTitleAsync(request.Title);
    }
}