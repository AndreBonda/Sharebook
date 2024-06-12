using ShareBook.Domain.Shared;

namespace ShareBook.Domain.Users;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User?> GetByEmailAsync(string email);
}