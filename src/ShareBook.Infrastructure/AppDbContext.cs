using Microsoft.EntityFrameworkCore;
using ShareBook.Application.Shared;
using ShareBook.Infrastructure.DataModel;

namespace ShareBook.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<UserData> Users { get; set; }
    public DbSet<BookData> Books { get; set; }
    public DbSet<LoanRequestData> LoanRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookData>()
            .HasOne(b => b.Owner)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        base.OnModelCreating(modelBuilder);
    }
}