using Bookstore.Models.Contracts.DTOs;

namespace Bookstore.Services.Interfaces
{
    public interface IBookService
    {
        Task<BaseResponse<BookResponse>> AddBookAsync(AddBookRequest request);
        Task<BaseResponse<IEnumerable<BookResponse>>> GetAllBooksAsync();
        Task<BaseResponse<BookResponse>> GetBookByIdAsync(int id);
        Task<BaseResponse<BookResponse>> UpdateStockQuantityAsync(int bookId, int newStockQuantity);
    }
}
