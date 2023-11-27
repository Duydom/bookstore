using BookStore.DTOs.Book;
using BookStore.DTOs.User;

namespace BookStore.DTOs.Rate
{
    public class RatingDTO
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Create { get; set; }
        public virtual UserDTO User { get; set; }
        public virtual BookDTO Book { get; set; }
    }
}
