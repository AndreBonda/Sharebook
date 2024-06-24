using System.ComponentModel.DataAnnotations;

namespace ShareBook.Infrastructure.DataModel;

public sealed class UserData
{
    public Guid Id { get; set; }
    [MaxLength(100)]
    public required string Email { get; set; }
    [MaxLength(100)]
    public required string Password { get; set; }
    public required DateTime CreatedAt { get; set; }
    public ICollection<UserData> Books { get; set; } = new List<UserData>();
}