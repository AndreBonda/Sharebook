using MediatR;

namespace ShareBook.Application.Books;

public record GetBooksQuery(string Title) : IRequest<IEnumerable<BookVM>>;