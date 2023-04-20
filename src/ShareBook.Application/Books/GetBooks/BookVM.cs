using MediatR;

namespace ShareBook.Application.Books.GetBooks;

public record BookVM(
    Guid Id,
    string Owner,
    string Title,
    string Author,
    int Pages,
    IEnumerable<string> Labels
);