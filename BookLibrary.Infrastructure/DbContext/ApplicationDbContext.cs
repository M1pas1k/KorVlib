using BookLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BookLibrary.Infrastructure.DbContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<BookCollection> BookCollections { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<UserBookStatus> UserBookStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasIndex(b => b.ISBN).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        modelBuilder.Entity<BookCollection>()
            .HasOne(bc => (User)bc.User)
            .WithMany()
            .HasForeignKey(bc => bc.UserId);

        modelBuilder.Entity<Review>()
            .HasOne(r => (Book)r.Book)
            .WithMany()
            .HasForeignKey(r => r.BookId);

        modelBuilder.Entity<Review>()
            .HasOne(r => (User)r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId);

        modelBuilder.Entity<UserBookStatus>()
            .HasIndex(ubs => new { ubs.UserId, ubs.BookId })
            .IsUnique();
    }
}