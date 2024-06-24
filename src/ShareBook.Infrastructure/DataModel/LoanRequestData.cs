using ShareBook.Domain.Books;
using ShareBook.Domain.Users;

namespace ShareBook.Infrastructure.DataModel;

public class LoanRequestData
{
    public required Guid Id { get; set; }
    public required LoanRequest.LoanRequestStatus Status { get; set; }
    public required Guid UserId { get; set; }
    public UserData User { get; set; } = null!;
    public required Guid BookId { get; set; }
    public BookData Book { get; set; } = null!;
}