using Microsoft.EntityFrameworkCore;
using ShareBook.Domain.Books;
using ShareBook.Domain.Shippings;

namespace ShareBook.Infrastructure.Repositories;

public class ShippingRepository : BaseRepository<Shipping, Guid>, IShippingRepository
{
    public ShippingRepository(AppDbContext ctx) : base(ctx)
    {
    }

    public async override Task AddAsync(Shipping entity)
    {
        await _ctx.Shippings.AddAsync(entity);
    }

    public override async Task<Shipping?> GetByIdAsync(Guid id) =>
        await _ctx.Shippings.FirstOrDefaultAsync(b => b.Id == id);
}