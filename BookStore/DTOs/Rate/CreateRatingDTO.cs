namespace BookStore.DTOs.Rate
{
    public class CreateRatingDTO
    {
        public int Rate { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}
