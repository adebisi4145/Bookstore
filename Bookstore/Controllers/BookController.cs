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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookByIdAsync(int id)
        {
            var response = await _bookService.GetBookByIdAsync(id);

            if (!response.Status)
                return NotFound(response.Message);

            return Ok(response.Data);
        }
        [HttpPut("update-stock-quantity/{bookId}")]
        public async Task<IActionResult> UpdateStockQuantityAsync(int bookId, [FromBody] int newStockQuantity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _bookService.UpdateStockQuantityAsync(bookId, newStockQuantity);

            if (!response.Status)
                return BadRequest(response.Message); 

            return Ok(response.Data);
        }
    }
}

