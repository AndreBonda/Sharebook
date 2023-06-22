using Microsoft.EntityFrameworkCore;
using ShareBook.Domain.Books;
using ShareBook.Domain.Users;

namespace ShareBook.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User, Guid>, IUserRepository
{
    public UserRepository(AppDbContext ctx) : base(ctx)
    {
    }

    public async override Task AddAsync(User entity)
    {
        await _ctx.Users.AddAsync(entity);
    }

    public async Task<User> GetByEmailAsync(string email) =>
        await _ctx.Users.FirstOrDefaultAsync(u => u.Email.Value == email);

    public override async Task<User> GetByIdAsync(Guid id) => 
        await _ctx.Users.FirstOrDefaultAsync(b => b.Id == id);
}