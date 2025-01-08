using Bookstore.Models.Entities;

namespace Bookstore.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateStockAsync(Book book);
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(Guid id);
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author);
        Task<IEnumerable<Book>> GetBooksByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<Book?> GetBookByTitleAndAuthorAsync(string title, string author);
    }
}
