using MediatR;
using ShareBook.Application.Books.ViewModels;

namespace ShareBook.Application.Books;

public class GetBookHandler(IBookQueries bookQueries) : IRequestHandler<GetBookQuery, BookVM?>
{
    public async Task<BookVM?> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        return await bookQueries.GetBookByIdAsync(request.Id);
    }
}