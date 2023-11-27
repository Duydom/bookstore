using BookStore.Model;

namespace BookStore.Repositories.BookRepository
{
    public interface IBookRepository
    {
        List<Book> GetBooks(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID", int? tagId = 0);
        List<Book> GetCart(List<int> bookIds);
        Book GetBookById(int id);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        void CreateBook(Book book);
        int GetBookCount();
        bool IsSaveChanges();
    }
}
