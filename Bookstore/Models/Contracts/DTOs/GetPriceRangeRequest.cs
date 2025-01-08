using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models.Contracts.DTOs
{
    public class GetPriceRangeRequest
    {
        [Required(ErrorMessage = "Minimum price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Minimum price must be greater than 0")]
        public decimal MinPrice { get; set; }
        [Required(ErrorMessage = "Maximum price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Maximum price must be greater than 0")]
        public decimal MaxPrice { get; set; }
    }
}
