using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    public class OrderBook
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; } = 0;
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public OrderBook() { }
        public OrderBook(int quantity, int orderId, int bookId)
        {
            Quantity = quantity;
            OrderId = orderId;
            BookId = bookId;
        }
    }
}
