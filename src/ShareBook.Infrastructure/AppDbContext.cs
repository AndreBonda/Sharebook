using Microsoft.EntityFrameworkCore;
using ShareBook.Application.Shared;
using ShareBook.Domain.Books;

namespace ShareBook.Infrastructure;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<LoanRequest> LoanRequests => Set<LoanRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .Property(e => e.Labels)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
            );

        // one-to-one fk: loanRequest --> Book
        modelBuilder.Entity<Book>()
            .OwnsOne<LoanRequest>("CurrentLoanRequest", x =>
            {
                x.WithOwner().HasForeignKey("BookId");
            });

        base.OnModelCreating(modelBuilder);
    }
}