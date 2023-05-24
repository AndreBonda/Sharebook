using MediatR;

namespace ShareBook.Application.Books;

public record CreateBookCmd(
    Guid Id, 
    string CurrentUser,
    string Title,
    string Author,
    int Pages,
    bool SharedByOwner,
    IEnumerable<string> Labels) : IRequest;