using BookStore.DTOs.Book;
using BookStore.DTOs.CartBook;

namespace BookStore.DTOs.Cart
{
    public class CartDTO
    {
        public List<CartBookDTO> CartBooks { get; set; }
        public CartDTO() { }
    }
}
