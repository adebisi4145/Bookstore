using Bookstore.Models.Contracts.DTOs;

namespace Bookstore.Services.Interfaces
{
    public interface IBookService
    {
        Task<BaseResponse<BookResponse>> AddBookAsync(AddBookRequest request);
        Task<BaseResponse<IEnumerable<BookResponse>>> GetAllBooksAsync();
        Task<BaseResponse<BookResponse>> GetBookByIdAsync(Guid id);
        Task<BaseResponse<IEnumerable<BookResponse>>> GetBooksByAuthorAsync(string author);
        Task<BaseResponse<IEnumerable<BookResponse>>> GetBooksByPriceRangeAsync(GetPriceRangeRequest request);
        Task<BaseResponse<BookResponse>> UpdateStockAsync(Guid bookId, UpdateStockRequest request);
    }
}
