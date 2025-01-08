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

        public async Task<Book?> GetBookByIdAsync(Guid id)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(c=> c.Id == id);
        }
        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author)
        {
            return await _dbContext.Books
                .Where(b => b.Author.ToLower().Contains(author.ToLower()))
                .ToListAsync();
        }
        public async Task<Book?> GetBookByTitleAndAuthorAsync(string title, string author)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(b => b.Title.ToLower() == title.ToLower() && b.Author.ToLower() == author.ToLower());
        }
        public async Task<IEnumerable<Book>> GetBooksByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _dbContext.Books
                .Where(b => b.Price >= minPrice && b.Price <= maxPrice)
                .ToListAsync();
        }

        public async Task<Book> UpdateStockAsync(Book book)
        {
             _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }
    }
}
