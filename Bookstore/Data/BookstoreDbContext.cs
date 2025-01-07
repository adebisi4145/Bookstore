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
        }
        public DbSet<Book> Books { get; set; }
    }
}
