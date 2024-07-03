using MediatR;
using ShareBook.Application.Books.ViewModels;

namespace ShareBook.Application.Books;

public record GetBooksQuery(string? Title, int Offset = 0, int Limit = 20) : IRequest<IEnumerable<BookVM>>;