using MediatR;

namespace ShareBook.Application.Books.GetBooks;

public record GetBooksQuery(string Title) : IRequest<IEnumerable<BookVM>>;