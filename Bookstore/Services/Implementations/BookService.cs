using Bookstore.Models.Contracts.DTOs;
using Bookstore.Models.Entities;
using Bookstore.Repositories.Interfaces;
using Bookstore.Services.Interfaces;

namespace Bookstore.Services.Implementations
{
    public class BookService: IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository bookRepository, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<BookResponse>> AddBookAsync(AddBookRequest request)
        {
            var response = new BaseResponse<BookResponse>();
            try
            {
                _logger.LogInformation("Adding a new book");
                var existingBook = await _bookRepository.GetBookByTitleAndAuthorAsync(request.Title, request.Author);
                if (existingBook != null)
                {
                    response.Status = false;
                    response.Message = "A book with the same title and author already exists";
                    _logger.LogWarning("Attempted to add a duplicate book");
                    return response;
                }

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
                _logger.LogInformation("Book successfully created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding book");
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
                _logger.LogInformation("Fetching all books from the database");

                var books = await _bookRepository.GetAllBooksAsync();
                if (books == null || !books.Any())
                {
                    response.Status = false;
                    response.Message = "No books found";
                    _logger.LogWarning("No books found in the database");
                    return response;
                }
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
                _logger.LogInformation("Books successfully retrieved from the database");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching books");
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<BookResponse>> GetBookByIdAsync(Guid id)
        {
            var response = new BaseResponse<BookResponse>();

            try
            {
                _logger.LogInformation("Fetching book with the ID in the database");

                var book = await _bookRepository.GetBookByIdAsync(id);

                if (book == null)
                {
                    response.Status = false;
                    response.Message = "Book not found";
                    _logger.LogWarning("No book found with this ID found in the database");
                    return response;
                }
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
                _logger.LogInformation("Book successfully retrieved by ID");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the book with ID");
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<BookResponse>>> GetBooksByAuthorAsync(string author)
        {
            var response = new BaseResponse<IEnumerable<BookResponse>>();

            try
            {
                _logger.LogInformation("Fetching books by this author from the database");

                var books = await _bookRepository.GetBooksByAuthorAsync(author);
                if (books == null || !books.Any())
                {
                    response.Status = false;
                    response.Message = "No books found by this author";
                    _logger.LogWarning("No books by this author found in the database");
                    return response;
                }
                var bookResponses = books.Select(book => new BookResponse
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Price = book.Price,
                    StockQuantity = book.StockQuantity
                }).ToList();

                response.Data = bookResponses;
                response.Status = true;
                response.Message = "Books successfully retrieved by author";
                _logger.LogInformation("Books successfully retrieved by author");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching books by author");
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<BookResponse>>> GetBooksByPriceRangeAsync(GetPriceRangeRequest request)
        {
            var response = new BaseResponse<IEnumerable<BookResponse>>();

            try
            {
                _logger.LogInformation("Fetching books within the price range from the database");
                
                if (request.MinPrice > request.MaxPrice)
                {
                    response.Status = false;
                    response.Message = "Minimum price cannot be greater than maximum price";
                    _logger.LogWarning("Minimum price is greater than maximum price");
                    return response;
                }
                var books = await _bookRepository.GetBooksByPriceRangeAsync(request.MinPrice, request.MaxPrice);

                if (books == null || !books.Any())
                {
                    response.Status = false;
                    response.Message = "No books found within the specified price range";
                    _logger.LogWarning("No books within the price range found in the database");
                }
                else
                {
                    var bookResponses = books.Select(book => new BookResponse
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        Price = book.Price,
                        StockQuantity = book.StockQuantity
                    }).ToList();

                    response.Data = bookResponses;
                    response.Status = true;
                    response.Message = "Books successfully retrieved within price range";
                    _logger.LogInformation("Books successfully retrieved within price range");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching books within price range");
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<BookResponse>> UpdateStockAsync(Guid bookId, UpdateStockRequest request)
        {
            var response = new BaseResponse<BookResponse>();

            try
            {
                _logger.LogInformation("Updating stock");

                var book = await _bookRepository.GetBookByIdAsync(bookId);

                if (book == null)
                {
                    response.Status = false;
                    response.Message = "Book not found";
                    _logger.LogWarning("Book not found");
                    return response;
                }

                book.StockQuantity = request.NewStock;
                await _bookRepository.UpdateStockAsync(book);

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
                _logger.LogInformation("Stock successfully updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating stock");
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
