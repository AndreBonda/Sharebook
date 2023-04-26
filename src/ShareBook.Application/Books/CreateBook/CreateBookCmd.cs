using MediatR;

namespace ShareBook.Application.Books.CreateBook;

public record CreateBookCmd(
    Guid Id, 
    string Owner,
    string Title,
    string Author,
    int Pages,
    IEnumerable<string> Labels) : IRequest;