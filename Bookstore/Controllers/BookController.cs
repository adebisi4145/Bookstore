using Bookstore.Models.Contracts.DTOs;
using Bookstore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBookAsync([FromBody] AddBookRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _bookService.AddBookAsync(request);

            if (!response.Status)
                return BadRequest(response.Message);

            return Ok(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var response = await _bookService.GetAllBooksAsync();

            if (!response.Status)
                return BadRequest(response.Message);

            return Ok(response.Data);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetBookByIdAsync(Guid id)
        {
            var response = await _bookService.GetBookByIdAsync(id);

            if (!response.Status)
                return NotFound(response.Message);

            return Ok(response.Data);
        }

        [HttpGet("get-by-author{author}")]
        public async Task<IActionResult> GetBookByAuthorAsync(string author)
        {
            var response = await _bookService.GetBooksByAuthorAsync(author);

            if (!response.Status)
                return NotFound(response.Message);

            return Ok(response.Data);
        }

        [HttpGet("get-by-price-range")]
        public async Task<IActionResult> GetBooksByPriceRangeAsync([FromQuery] GetPriceRangeRequest request)
        {
            var response = await _bookService.GetBooksByPriceRangeAsync(request);

            if (!response.Status)
                return NotFound(response.Message);

            return Ok(response.Data);
        }

        [HttpPut("update-stock/{bookId}")]
        public async Task<IActionResult> UpdateStockAsync(Guid bookId, [FromBody] UpdateStockRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _bookService.UpdateStockAsync(bookId, request);

            if (!response.Status)
                return BadRequest(response.Message); 

            return Ok(response.Data);
        }
    }
}

