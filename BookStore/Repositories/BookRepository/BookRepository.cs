using BookStore.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.BookRepository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _dataContext;

        public static int Total = 0;
        public BookRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void CreateBook(Book book)
        {
            _dataContext.Books.Add(book);
        }

        public void DeleteBook(Book book)
        {
            _dataContext.Books.Remove(book);
        }

        public Book GetBookById(int id)
        {
            return _dataContext.Books.Include(a => a.Author).Include(p => p.Publisher).Include(r => r.Ratings).Include(t => t.Tags).FirstOrDefault(b => b.Id == id);
        }

        public int GetBookCount()
        {
            return _dataContext.Books.Count();
        }

        public List<Book> GetBooks(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID", int? tagId = 0)
        {
            var query = _dataContext.Books.Include(a => a.Author).Include(p => p.Publisher).Include(t => t.Tags).Include(r => r.Ratings).AsQueryable();
            
            var tag = _dataContext.Tags.FirstOrDefault(t => t.Id == tagId);
            
            if (tag != null)
            {
                query = _dataContext.Books.Include(a => a.Author).Include(p => p.Publisher).Include(t => t.Tags).Include(r => r.Ratings).Where(b => b.Tags.FirstOrDefault(t => t.Id == tagId) != null).AsQueryable();
            }

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(au => au.Title.ToLower().Contains(key.ToLower()));
            }

            switch (sortBy)
            {
                case "TITLE":
                    query = query.OrderBy(u => u.Title);
                    break;
                case "PRICE":
                    query = query.OrderBy(u => u.Price);
                    break;
                case "PRICE_DEC":
                    query = query.OrderByDescending(u => u.Price);
                    break;
                case "PUBLISHDATE":
                    query = query.OrderBy(u => u.PublishDate);
                    break;
                default:
                    query = query.OrderBy(u => u.IsDeleted).ThenBy(u => u.Id);
                    break;
            }

            Total = query.Count();

            if (page == null || pageSize == null || sortBy == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
        }

        public List<Book> GetCart(List<int> bookIds)
        {
            List<Book> books = new List<Book>();
            foreach (var bookId in bookIds)
            {
                var book = _dataContext.Books.FirstOrDefault(b => b.Id == bookId);
                if (book != null)
                    books.Add(book);
            }
            return books;
        }

        public bool IsSaveChanges()
        {
            return _dataContext.SaveChanges() > 0;
        }

        public void UpdateBook(Book book)
        {
            _dataContext.Entry(book).State = EntityState.Modified;
        }
    }
}
