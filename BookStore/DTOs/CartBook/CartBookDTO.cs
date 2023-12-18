using BookStore.DTOs.Book;

namespace BookStore.DTOs.CartBook
{
    public class CartBookDTO
    {
        public BookDTO Book { get; set; }
        public int Quantity { get; set; }
    }
}
