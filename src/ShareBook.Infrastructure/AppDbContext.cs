using Microsoft.EntityFrameworkCore;
using ShareBook.Application.Shared;
using ShareBook.Domain.Books;
using ShareBook.Domain.Shared;
using ShareBook.Domain.Shared.Primitives;
using ShareBook.Domain.Shippings;

namespace ShareBook.Infrastructure;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<LoanRequest> LoanRequests => Set<LoanRequest>();
    public DbSet<Shipping> Shippings => Set<Shipping>();

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
}