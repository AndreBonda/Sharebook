using MediatR;
using ShareBook.Application.Books.ViewModels;

namespace ShareBook.Application.Books;

public class GetBookHandler : IRequestHandler<GetBookQuery, BookVM>
{
    private readonly IBookQueries _bookQueries;

    public GetBookHandler(IBookQueries bookQueries)
    {
        _bookQueries = bookQueries;
    }

    public async Task<BookVM> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        return await _bookQueries.GetBookByIdAsync(request.Id);
    }
}