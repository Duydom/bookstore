using BookStore.DTOs.Response;
using BookStore.Model;

namespace BookStore.Service.AuthorService
{
    public interface IAuthorService
    {
        ResponseDTO GetAuthors(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        ResponseDTO GetAuthorById(int id);
        ResponseDTO UpdateAuthor(int id, string name);
        ResponseDTO DeleteAuthor(int id);
        ResponseDTO CreateAuthor(string name);
    }
}
