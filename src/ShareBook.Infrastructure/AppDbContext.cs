using Microsoft.EntityFrameworkCore;
using ShareBook.Application.Shared;
using ShareBook.Domain.Books;
using ShareBook.Domain.Shared;
using ShareBook.Domain.Shared.Primitives;
using ShareBook.Domain.Shippings;

namespace ShareBook.Infrastructure;

public class AppDbContext : DbContext, IAppDbContext
{
    private readonly DomainEventDispatcher _domainEventDispatcher;

    public AppDbContext(DbContextOptions<AppDbContext> options, DomainEventDispatcher domainEventDispatcher = null) : base(options)
    {
        _domainEventDispatcher = domainEventDispatcher;
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<LoanRequest> LoanRequests => Set<LoanRequest>();
    public DbSet<Shipping> Shippings => Set<Shipping>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken);

        if (_domainEventDispatcher is not null)
        {
            var eventContainers = GetEventContainers();
            await _domainEventDispatcher.DispatchAndClearEventsAsync(eventContainers);
        }

        return result;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .Property(e => e.Labels)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
            );

        modelBuilder.Entity<Book>()
            .OwnsOne<LoanRequest>("CurrentLoanRequest");

        modelBuilder.Entity<Book>()
            .HasMany<Shipping>()
            .WithOne();

        base.OnModelCreating(modelBuilder);
    }

    private IEnumerable<IEventContainer> GetEventContainers() => 
        ChangeTracker.Entries<IEventContainer>()
        .Where(ec => ec.Entity.Events().Any())
        .Select(ec => ec.Entity);
}