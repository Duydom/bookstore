using BookStore.Model;

namespace BookStore.Repositories.AuthorRepository
{
    public interface IAuthorRepository
    {
        List<Author> GetAuthors(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        Author GetAuthorById(int id);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
        void CreateAuthor(Author author);
        int GetAuthorCount();
        bool IsSaveChanges();
    }
}
