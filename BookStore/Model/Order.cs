using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; } = "NEW";
        [StringLength(255)]
        public string Description { get; set; } = "";
        public bool IsDeleted { get; set; } = false;
        public DateTime Create { get; set; } = DateTime.Now;
        public DateTime Update { get; set; } = DateTime.Now;
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        public int ShippingModeId { get; set; }
        public virtual ShippingMode ShippingMode { get; set; }
        [Required]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<OrderBook> OrderBooks { get; set; } = new List<OrderBook>();
    }
}
