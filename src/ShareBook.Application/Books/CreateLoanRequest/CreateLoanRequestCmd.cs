using MediatR;

namespace ShareBook.Application.Books.CreateBook;

public record CreateLoanRequestCmd(
    Guid BookId,
    string RequestingUser
    ) : IRequest;