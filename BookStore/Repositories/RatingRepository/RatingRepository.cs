using BookStore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;

namespace BookStore.Repositories.RatingRepository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly DataContext _dataContext;
        public RatingRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void CreateRating(Rating rating)
        {
            _dataContext.Ratings.Add(rating);
        }

        public void DeleteRating(Rating rating)
        {
            _dataContext.Ratings.Remove(rating);
        }

        public Rating GetRatingById(int id)
        {
            return _dataContext.Ratings.Include(r => r.User).Include(r => r.Book).FirstOrDefault(t => t.Id == id);
        }

        public List<Rating> GetRatingByBook(int bookId, int? page = 1)
        {
            var query = _dataContext.Ratings.Include(r => r.User).Where(r=>r.Book.Id == bookId).AsQueryable();

            return query.Skip((page.Value - 1) * 10000).Take(10).ToList();
        }

        public int GetRatingCount()
        {
            return _dataContext.Ratings.Count();
        }
        public double GetRatingAverage()
        {
            int total = 0;
            foreach(var rate in _dataContext.Ratings)
            {
                total += rate.Rate;
            }
            return total / _dataContext.Ratings.Count();
        }

        public List<Rating> GetRatings(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var query = _dataContext.Ratings.Include(r => r.User).Include(r => r.Book).AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(au => au.Comment.ToLower().Contains(key.ToLower()));
            }

            switch (sortBy)
            {
                case "RATE":
                    query = query.OrderBy(u => u.Rate);
                    break;

                case "CREATE":
                    query = query.OrderBy(u => u.Create);
                    break;

                default:
                    query = query.OrderBy(u => u.IsDeleted).ThenBy(u => u.Id);
                    break;
            }
            if (page == null || pageSize == null || sortBy == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
        }

        public bool IsSaveChanges()
        {
            return _dataContext.SaveChanges() > 0;
        }

        public void UpdateRating(Rating rating)
        {
            _dataContext.Entry(rating).State = EntityState.Modified;
        }
    }
}
