namespace Bookstore.Models.DTOs.Response_DTOs
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
