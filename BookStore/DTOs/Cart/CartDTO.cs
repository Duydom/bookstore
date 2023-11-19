using BookStore.DTOs.Book;

namespace BookStore.DTOs.Cart
{
    public class CartDTO
    {
        public List<QuantityDTO> Quantities { get; set; }
        public List<BookDTO> Books { get; set; }
    }
}
