using ShareBook.Domain.Books;
using ShareBook.Domain.Shared;

namespace ShareBook.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
{
    protected readonly AppDbContext _ctx;

    protected BaseRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public abstract Task AddAsync(TEntity entity);

    public abstract Task<TEntity?> GetByIdAsync(TPrimaryKey id);
}