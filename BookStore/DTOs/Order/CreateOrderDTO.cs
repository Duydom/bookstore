using BookStore.DTOs.Address;
using BookStore.DTOs.Book;
using BookStore.DTOs.ShippingMode;
using BookStore.DTOs.User;
using BookStore.Model;
using System.ComponentModel.DataAnnotations;

namespace BookStore.DTOs.Order
{
    public class CreateOrderDTO
    {
        public string Status { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int ShippingModeId { get; set; }
        public int AddressId { get; set; }
        public List<int> QuantitieCounts { get; set; }
        public virtual List<int> BookIds { get; set; }
    }
}
