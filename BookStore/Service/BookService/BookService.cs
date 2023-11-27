using AutoMapper;
using BookStore.DTOs.Book;
using BookStore.DTOs.Response;
using BookStore.Model;
using BookStore.Repositories.AuthorRepository;
using BookStore.Repositories.BookRepository;
using BookStore.Repositories.PublisherRepository;
using BookStore.Repositories.TagRepository;

namespace BookStore.Service.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IMapper _mapper;
        public BookService(IBookRepository bookRepository, IMapper mapper, ITagRepository tagRepository,
            IAuthorRepository authorRepository, IPublisherRepository publisherRepository)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _tagRepository = tagRepository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
        }
        public ResponseDTO CreateBook(CreateBookDTO createBookDTO)
        {
            var author = _authorRepository.GetAuthorById(createBookDTO.AuthorId);
            if (author == null) return new ResponseDTO()
            {
                Code = 400,
                Message = "Author không tồn tại"
            };
            var publisher = _publisherRepository.GetPublisherById(createBookDTO.PublisherId);
            if (publisher == null) return new ResponseDTO()
            {
                Code = 400,
                Message = "Author không tồn tại"
            };
            var book = _mapper.Map<Book>(createBookDTO);
            foreach (var tagId in createBookDTO.TagIds)
            {
                Tag tag = _tagRepository.GetTagById(tagId);
                if (tag != null)
                    book.Tags.Add(tag);
            }
            if (book.Tags.Count == 0) return new ResponseDTO()
            {
                Code = 400,
                Message = "Tag không được để trống"
            };
            _bookRepository.CreateBook(book);

            if (_bookRepository.IsSaveChanges())
                return new ResponseDTO()
                {
                    Message = "Tạo thành công"
                };
            else
                return new ResponseDTO()
                {
                    Data = 400,
                    Message = "Tạo thất bại"
                };
        }

        public ResponseDTO DeleteBook(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null) return new ResponseDTO()
            {
                Code = 400,
                Message = "Book không tồn tại"
            };

            book.IsDeleted = true;

            _bookRepository.UpdateBook(book);
            if (_bookRepository.IsSaveChanges())
                return new ResponseDTO()
                {
                    Message = "Xóa thành công"
                };
            else
                return new ResponseDTO()
                {
                    Data = 400,
                    Message = "Xóa thất bại"
                };
        }

        public ResponseDTO GetBookById(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null) return new ResponseDTO()
            {
                Code = 400,
                Message = "Book không tồn tại"
            };

            return new ResponseDTO()
            {
                Data = _mapper.Map<BookDTO>(book)
            };
        }

        public ResponseDTO GetBooks(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID", int? tagId = 0)
        {
            var books = _bookRepository.GetBooks(page, pageSize, key, sortBy, tagId);

            return new ResponseDTO()
            {
                Data = _mapper.Map<List<BookDTO>>(books),
                Total = BookRepository.Total
            };
        }

        public ResponseDTO GetCart(List<int> bookIds)
        {
            var books = _bookRepository.GetCart(bookIds);

            return new ResponseDTO()
            {
                Data = _mapper.Map<List<BookDTO>>(books),
                Total = _bookRepository.GetBookCount()
            };
        }

        public ResponseDTO UpdateBook(int id, UpdateBookDTO updateBookDTO)
        {

            var book = _bookRepository.GetBookById(id);
            if (book == null) return new ResponseDTO()
            {
                Code = 400,
                Message = "Book không tồn tại"
            };

            book.Update = DateTime.Now;
            book.Title = updateBookDTO.Title;
            book.Description = updateBookDTO.Description;
            book.NumberOfPages = updateBookDTO.NumberOfPages;
            book.PublishDate = updateBookDTO.PublishDate;
            book.Language = updateBookDTO.Language;
            book.Count = updateBookDTO.Count;
            book.Price = updateBookDTO.Price;
            book.Image = updateBookDTO.Image;
            book.PublisherId = updateBookDTO.PublisherId;
            book.AuthorId = updateBookDTO.AuthorId;

            book.Tags = new List<Tag>();
            foreach (var tagId in updateBookDTO.TagIds)
            {
                Tag tag = _tagRepository.GetTagById(tagId);
                if (tag != null)
                    book.Tags.Add(tag);
            }

            _bookRepository.UpdateBook(book);
            if (_bookRepository.IsSaveChanges())
                return new ResponseDTO()
                {
                    Message = "Cập nhật thành công"
                };
            else
                return new ResponseDTO()
                {
                    Data = 400,
                    Message = "Cập nhật thất bại"
                };
        }
    }
}
