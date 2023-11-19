using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Create { get; set; } = DateTime.Now;
        public DateTime Update { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public virtual List<Book> Books { get; set; } = new List<Book>();
    }
}
