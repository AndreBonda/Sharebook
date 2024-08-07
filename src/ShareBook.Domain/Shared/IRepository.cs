using ShareBook.Domain.Books;

namespace ShareBook.Domain.Shared;

public interface IRepository<Entity, PrimaryKey>
{
    Task<Entity?> GetByIdAsync(PrimaryKey id);
    Task AddAsync(Entity entity);
}