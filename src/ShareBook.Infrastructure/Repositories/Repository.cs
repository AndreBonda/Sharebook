using ShareBook.Domain.Books;
using ShareBook.Domain.Shared;

namespace ShareBook.Infrastructure.Repositories;

public abstract class BaseRepository<Entity, PrimaryKey> : IRepository<Entity, PrimaryKey>
{
    protected readonly AppDbContext _ctx;

    public BaseRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public abstract Task AddAsync(Entity entity);

    public abstract Task<Entity?> GetByIdAsync(PrimaryKey id);

    public async Task SaveAsync() => await _ctx.SaveChangesAsync();
}