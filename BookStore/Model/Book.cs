using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [Required]
        //[StringLength(255)]
        public string Description { get; set; }
        [Required]
        public int NumberOfPages { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;
        [Required]
        public string Language { get; set; }
        public int Count { get; set; } = 0;
        public double Price { get; set; } = 0;
        [Required]
        [StringLength(255)]
        public string Image { get; set; }
        public DateTime Create { get; set; } = DateTime.Now;
        public DateTime Update { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        [Required]
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public virtual List<Tag> Tags { get; set; } = new List<Tag>();
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
        public virtual List<OrderBook> OrderBooks { get; set; } = new List<OrderBook>();
        public virtual List<CartBook> CartBooks { get; set; } = new List<CartBook>();
    }
}
