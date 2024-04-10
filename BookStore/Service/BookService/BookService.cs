using AutoMapper;
using BookStore.DTOs.Book;
using BookStore.DTOs.Response;
using BookStore.Model;
using BookStore.Repositories.AuthorRepository;
using BookStore.Repositories.BookRepository;
using BookStore.Repositories.PublisherRepository;
using BookStore.Repositories.TagRepository;
using System.Collections.Generic;

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
            var shoe = _mapper.Map<Book>(createBookDTO);
            foreach (var tagId in createBookDTO.TagIds)
            {
                Tag tag = _tagRepository.GetTagById(tagId);
                if (tag != null)
                    shoe.Tags.Add(tag);
            }
            if (shoe.Tags.Count == 0) return new ResponseDTO()
            {
                Code = 400,
                Message = "Tag không được để trống"
            };
            _bookRepository.CreateBook(shoe);

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
            var shoe = _bookRepository.GetBookById(id);
            if (shoe == null) return new ResponseDTO()
            {
                Code = 400,
                Message = "Book không tồn tại"
            };

            shoe.IsDeleted = true;

            _bookRepository.UpdateBook(shoe);
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
            var shoe = _bookRepository.GetBookById(id);
            if (shoe == null) return new ResponseDTO()
            {
                Code = 400,
                Message = "Book không tồn tại"
            };

            return new ResponseDTO()
            {
                Data = _mapper.Map<BookDTO>(shoe)
            };
        }
        public ResponseDTO GetBookByIds(List<int> ids)
        {
            var shoes = new List<Book>();
            foreach (int id in ids)
            {
                var shoe = _bookRepository.GetBookById(id);
                if (shoe != null) shoes.Add(shoe);
            }
            if (shoes == null) return new ResponseDTO()
            {
                Code = 400,
                Message = "Book không tồn tại"
            };

            return new ResponseDTO()
            {
                Data = _mapper.Map<List<BookDTO>>(shoes)
            };
        }

        public ResponseDTO GetBooks(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID", int? tagId = 0)
        {
            var shoes = _bookRepository.GetBooks(page, pageSize, key, sortBy, tagId);

            return new ResponseDTO()
            {
                Data = _mapper.Map<List<BookDTO>>(shoes),
                Total = BookRepository.Total
            };
        }

        public ResponseDTO GetCart(List<int> shoeIds)
        {
            var shoes = _bookRepository.GetCart(shoeIds);

            return new ResponseDTO()
            {
                Data = _mapper.Map<List<BookDTO>>(shoes),
                Total = _bookRepository.GetBookCount()
            };
        }

        public ResponseDTO UpdateBook(int id, UpdateBookDTO updateBookDTO)
        {

            var shoe = _bookRepository.GetBookById(id);
            if (shoe == null) return new ResponseDTO()
            {
                Code = 400,
                Message = "Book không tồn tại"
            };

            shoe.Update = DateTime.Now;
            shoe.Title = updateBookDTO.Title;
            shoe.Description = updateBookDTO.Description;
            shoe.Size = updateBookDTO.Size;
            shoe.PublishDate = updateBookDTO.PublishDate;
            shoe.Language = updateBookDTO.Language;
            shoe.Count = updateBookDTO.Count;
            shoe.Price = updateBookDTO.Price;
            shoe.Image = updateBookDTO.Image;
            shoe.PublisherId = updateBookDTO.PublisherId;
            shoe.AuthorId = updateBookDTO.AuthorId;

            shoe.Tags = new List<Tag>();
            foreach (var tagId in updateBookDTO.TagIds)
            {
                Tag tag = _tagRepository.GetTagById(tagId);
                if (tag != null)
                    shoe.Tags.Add(tag);
            }

            _bookRepository.UpdateBook(shoe);
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
