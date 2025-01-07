using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models.Contracts.DTOs
{
    public class UpdateStockRequest
    {
        public int StockQuantity { get; set; }
    }
}
