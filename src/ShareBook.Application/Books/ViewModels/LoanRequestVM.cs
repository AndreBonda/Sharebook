using ShareBook.Domain.Books;

namespace ShareBook.Application.Books.ViewModels;

public class LoanRequestVM
{
    public required Guid Id { get; set; }
    public required LoanRequest.LoanRequestStatus Status { get; set; }
    public required Guid RequestingUserId { get; set; }
}