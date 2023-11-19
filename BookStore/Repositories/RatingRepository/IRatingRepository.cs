using BookStore.Model;

namespace BookStore.Repositories.RatingRepository
{
    public interface IRatingRepository
    {
        List<Rating> GetRatings(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        List<Rating> GetRatingByBook(int bookId, int? page = 1);
        Rating GetRatingById(int id);
        void UpdateRating(Rating rating);
        void DeleteRating(Rating rating);
        void CreateRating(Rating rating);
        int GetRatingCount();
        double GetRatingAverage();
        bool IsSaveChanges();
    }
}
