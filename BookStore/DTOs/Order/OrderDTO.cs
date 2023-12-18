using BookStore.DTOs.Address;
using BookStore.DTOs.Book;
using BookStore.DTOs.OrderBook;
using BookStore.DTOs.ShippingMode;
using BookStore.DTOs.User;
using System.ComponentModel.DataAnnotations;

namespace BookStore.DTOs.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public virtual UserDTO User { get; set; }
        public virtual ShippingModeDTO ShippingMode { get; set; }
        public virtual AddressDTO Address { get; set; }
        public virtual List<OrderBookDTO> OrderBooks { get; set; }
    }
}
