using Bookstore.Models.Entities;

namespace Bookstore.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateStockQuantityAsync(Book book);
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
    }
}
