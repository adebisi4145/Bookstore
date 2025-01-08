using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models.Contracts.DTOs
{
    public class UpdateStockRequest
    {
        [Required(ErrorMessage = "Stock is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        public int NewStock { get; set; }
    }
}
