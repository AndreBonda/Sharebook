using Microsoft.EntityFrameworkCore;
using ShareBook.Domain.Shared.ValueObjects;
using ShareBook.Domain.Users;
using ShareBook.Infrastructure.DataModel;

namespace ShareBook.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User, Guid>, IUserRepository
{
    public UserRepository(AppDbContext ctx) : base(ctx) { }

    public override async Task AddAsync(User entity)
    {
        await _ctx.Users.AddAsync(new UserData
        {
            Id = entity.Id,
            Email = entity.Email.Value,
            Password = entity.Password.PasswordHash,
            CreatedAt = entity.CreatedAt
        });
        await _ctx.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        UserData? userData = await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (userData is null) return null;

        return new User(
            userData.Id,
            new Email(userData.Email),
            new Password(userData.Password)
        );
    }

    public override async Task<User?> GetByIdAsync(Guid id)
    {
        UserData? userData = await _ctx.Users.FirstOrDefaultAsync(b => b.Id == id);

        if (userData is null) return null;

        return new User(
            userData.Id,
            new Email(userData.Email),
            new Password(userData.Password)
        );
    }
}