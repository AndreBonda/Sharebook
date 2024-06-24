using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ShareBook.Infrastructure.DataModel;

public sealed class BookData
{
    public required Guid Id { get; set; }
    public required Guid OwnerId { get; set; }
    public UserData Owner { get; set; } = null!;
    [MaxLength(100)]
    public required string Title { get; set; }
    [MaxLength(100)]
    public required string Author { get; set; }
    public required uint Pages { get; set; }
    public required bool SharedByOwner { get; set; }
    public required string[] Labels { get; set; }
    public ICollection<LoanRequestData> LoanRequests { get; set; } = new List<LoanRequestData>();
}