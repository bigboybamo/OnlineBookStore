namespace OnlineBookStore.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal Revenue { get; set; }
    }
}
