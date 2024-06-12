using Microsoft.EntityFrameworkCore;
using ShareBook.Application.Shared;
using ShareBook.Domain.Books;
using ShareBook.Domain.Shared.ValueObjects;
using ShareBook.Domain.Shippings;
using ShareBook.Domain.Users;

namespace ShareBook.Infrastructure;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<LoanRequest> LoanRequests => Set<LoanRequest>();
    public DbSet<Shipping> Shippings => Set<Shipping>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Book
        modelBuilder.Entity<Book>()
            .Property(e => e.Labels)
            .HasConversion(
                v => string.Join(',', v ?? Array.Empty<string>()),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        modelBuilder.Entity<Book>()
            .OwnsOne<LoanRequest>("CurrentLoanRequest");

        modelBuilder.Entity<Book>()
            .HasMany<Shipping>()
            .WithOne();
        #endregion

        #region User
        modelBuilder.Entity<User>()
            .OwnsOne<Email>(u => u.Email)
            .Property(e => e.Value)
            .HasColumnName("email");

        modelBuilder.Entity<User>()
            .OwnsOne<Password>("_password")
            .Property(p => p.PasswordHash)
            .HasColumnName("password");

        modelBuilder.Entity<User>()
            .HasMany<Book>()
            .WithOne()
            .HasForeignKey(b => b.OwnerId);
        #endregion

        base.OnModelCreating(modelBuilder);
    }
}