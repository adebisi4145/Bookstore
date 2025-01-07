using Bookstore.Models.Contracts.DTOs;
using Bookstore.Models.Entities;
using Bookstore.Repositories.Interfaces;
using Bookstore.Services.Interfaces;

namespace Bookstore.Services.Implementations
{
    public class BookService: IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BaseResponse<BookResponse>> AddBookAsync(AddBookRequest request)
        {
            var response = new BaseResponse<BookResponse>();
            try
            {
                var book = new Book
                {
                    Title = request.Title,
                    Author = request.Author,
                    Price = request.Price,
                    StockQuantity = request.StockQuantity
                };
                var newBook = await _bookRepository.AddBookAsync(book);

                var bookResponse = new BookResponse
                {
                    Id = newBook.Id,
                    Title = newBook.Title,
                    Author = newBook.Author,
                    Price = newBook.Price,
                    StockQuantity = newBook.StockQuantity
                };

                response.Data = bookResponse;
                response.Status = true;
                response.Message = "Book successfully created";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<BookResponse>>> GetAllBooksAsync()
        {
            var response = new BaseResponse<IEnumerable<BookResponse>>();

            try
            {
                var books = await _bookRepository.GetAllBooksAsync();
                var bookResponses = books.Select(b => new BookResponse
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Price = b.Price,
                    StockQuantity = b.StockQuantity
                }).ToList();

                response.Data = bookResponses;
                response.Status = true;
                response.Message = "Books successfully retrieved";
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<BookResponse>> GetBookByIdAsync(int id)
        {
            var response = new BaseResponse<BookResponse>();

            try
            {
                var book = await _bookRepository.GetBookByIdAsync(id);

                if (book == null)
                {
                    response.Status = false;
                    response.Message = "Book not found";
                }
                else
                {
                    var bookResponse = new BookResponse
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        Price = book.Price,
                        StockQuantity = book.StockQuantity
                    };

                    response.Data = bookResponse;
                    response.Status = true;
                    response.Message = "Book successfully retrieved";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }


        public async Task<BaseResponse<BookResponse>> UpdateStockAsync(int bookId, int newStockQuantity)
        {
            var response = new BaseResponse<BookResponse>();

            try
            {
                var book = await _bookRepository.GetBookByIdAsync(bookId);

                if (book == null)
                {
                    response.Status = false;
                    response.Message = "Book not found";
                }
                else if (newStockQuantity < 0)
                {
                    response.Status = false;
                    response.Message = "Stock cannot be negative";
                }
                else
                {
                    book.StockQuantity = newStockQuantity;
                    await _bookRepository.UpdateStockQuantityAsync(book);
                    var bookResponse = new BookResponse
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        Price = book.Price,
                        StockQuantity = book.StockQuantity
                    };

                    response.Data = bookResponse;
                    response.Status = true;
                    response.Message = "Stock successfully updated";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
