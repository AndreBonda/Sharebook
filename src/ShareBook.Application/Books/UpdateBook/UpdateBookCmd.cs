using MediatR;

namespace ShareBook.Application.Books;

public record UpdateBookCmd(
    Guid Id, 
    string CurrentUser,
    string Title,
    string Author,
    int Pages,
    bool SharedByOwner,
    IEnumerable<string> Labels) : IRequest;