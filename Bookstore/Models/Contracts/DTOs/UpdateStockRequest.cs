using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models.Contracts.DTOs
{
    public class UpdateStockRequest
    {
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        public int StockQuantity { get; set; }
    }
}
