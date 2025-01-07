using Bookstore.Data;
using Bookstore.Models.Entities;
using Bookstore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Repositories.Implemetations
{
    public class BookRepository: IBookRepository
    {
        private readonly BookstoreDbContext _dbContext;

        public BookRepository(BookstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            await _dbContext.AddAsync(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(c=> c.Id == id);
        }

        public async Task<Book> UpdateStockQuantityAsync(Book book)
        {
             _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }
    }
}
