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

    public override async Task<User> GetByIdAsync(Guid id) => 
        await _ctx.Users.FirstOrDefaultAsync(b => b.Id == id);
}