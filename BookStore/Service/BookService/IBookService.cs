using BookStore.DTOs.Book;
using BookStore.DTOs.Response;

namespace BookStore.Service.BookService
{
    public interface IBookService
    {
        ResponseDTO GetBooks(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID", int? tagId = 0);
        ResponseDTO GetBookById(int id);
        ResponseDTO GetBookByIds(List<int> ids);
        ResponseDTO UpdateBook(int id, UpdateBookDTO updateBookDTO);
        ResponseDTO DeleteBook(int id);
        ResponseDTO GetCart(List<int> bookIds);
        ResponseDTO CreateBook(CreateBookDTO createBookDTO);
    }
}
