using BookStore.DTOs.Book;

namespace BookStore.DTOs.OrderBook
{
    public class OrderBookDTO
    {
        public BookDTO Book { get; set; }
        public int Quantity { get; set; }
    }
}
