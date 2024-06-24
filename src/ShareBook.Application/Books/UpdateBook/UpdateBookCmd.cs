using MediatR;

namespace ShareBook.Application.Books;

public record UpdateBookCmd(
    Guid Id,
    Guid UserId,
    string Title,
    string Author,
    uint Pages,
    bool SharedByOwner,
    IEnumerable<string>? Labels) : IRequest;