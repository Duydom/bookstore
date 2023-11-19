using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    public class Quantity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
