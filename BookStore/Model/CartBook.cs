using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    public class CartBook
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; } = 0;
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public CartBook() { }
        public CartBook(int quantity, int cartId, int bookId)
        {
            Quantity = quantity;
            CartId = cartId;
            BookId = bookId;
        }
    }
}
