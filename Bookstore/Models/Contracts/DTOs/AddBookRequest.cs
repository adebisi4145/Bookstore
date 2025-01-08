using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models.Contracts.DTOs
{
    public class AddBookRequest
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Author { get; set; } = null!;
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative")]
        public int StockQuantity { get; set; }
    }
}
