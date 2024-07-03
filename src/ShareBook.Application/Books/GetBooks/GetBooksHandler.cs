using MediatR;
using ShareBook.Application.Books.ViewModels;

namespace ShareBook.Application.Books;

public class GetBooksHandler(IBookQueries bookQueries) : IRequestHandler<GetBooksQuery, IEnumerable<BookVM>>
{
    public async Task<IEnumerable<BookVM>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        return await bookQueries.GetBooksByTitleAsync(request.Title, request.Offset, request.Limit);
    }
}