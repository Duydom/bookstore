using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public DateTime Create { get; set; } = DateTime.Now;
        public DateTime Update { get; set; } = DateTime.Now;
        //public int UserId { get; set; }
        //public virtual User User { get; set; }
        public virtual List<CartBook> CartBooks { get; set; } = new List<CartBook>();
    }
}
