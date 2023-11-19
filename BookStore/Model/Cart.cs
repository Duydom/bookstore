using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public DateTime Create { get; set; } = DateTime.Now;
        public DateTime Update { get; set; } = DateTime.Now;
        public virtual List<Book> Books { get; set; } = new List<Book>();
        public virtual List<Quantity> Quantities { get; set; } = new List<Quantity>();
    }
}
