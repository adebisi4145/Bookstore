using Bookstore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Data
{
    public class BookstoreDbContext: DbContext
    {
        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options): base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
            .HasKey(b => b.Id);

            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Book>()
                .Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .IsRequired()
                .HasPrecision(18, 2) 
                .HasDefaultValue(0.01);

            modelBuilder.Entity<Book>()
                .Property(b => b.StockQuantity)
                .IsRequired() 
                .HasDefaultValue(0);
        }
        public DbSet<Book> Books { get; set; }
    }
}
