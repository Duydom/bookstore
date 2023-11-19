using AutoMapper;
using BookStore.DTOs.Rate;
using BookStore.DTOs.Response;
using BookStore.Model;
using BookStore.Repositories.BookRepository;
using BookStore.Repositories.RatingRepository;
using BookStore.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;

namespace BookStore.Service.RatingService
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public RatingService(IRatingRepository ratingRepository, IMapper mapper, IBookRepository bookRepository, IUserRepository userRepository)
        {
            _ratingRepository = ratingRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public ResponseDTO CreateRating(CreateRatingDTO createRatingDTO)
        {
            if (createRatingDTO.Rate <= 0 || createRatingDTO.Rate > 5) return new ResponseDTO() { Code = 400, Message = "Giá trị rate không hợp lệ" };
            var user = _userRepository.GetUserById(createRatingDTO.UserId);
            if (user == null) return new ResponseDTO() { Code = 400, Message = "User không tồn tại" };

            var book = _bookRepository.GetBookById(createRatingDTO.BookId);
            if (book == null) return new ResponseDTO() { Code = 400, Message = "Book không tồn tại" };

            var rating = _mapper.Map<Rating>(createRatingDTO);

            _ratingRepository.CreateRating(rating);
            if (_ratingRepository.IsSaveChanges()) return new ResponseDTO() { Message = "Tạo thành công" };
            else return new ResponseDTO() { Code = 400, Message = "Tạo thất bại" };
        }

        public ResponseDTO DeleteRating(int id)
        {
            var rating = _ratingRepository.GetRatingById(id);

            if (rating == null)
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Rating không tồn tại"
                };

            rating.IsDeleted = true;
            _ratingRepository.UpdateRating(rating);

            if (_ratingRepository.IsSaveChanges()) return new ResponseDTO() { Message = "Cập nhật thành công" };
            else return new ResponseDTO() { Code = 400, Message = "Cập nhật thất bại" };
        }

        public ResponseDTO GetRatingById(int id)
        {
            var rating = _ratingRepository.GetRatingById(id);
            if (rating == null)
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Rating không tồn tại"
                };
            return new ResponseDTO()
            {
                Data = _mapper.Map<RatingDTO>(rating)
            };
        }

        public ResponseDTO GetRatingByBook(int bookId, int? page = 1)
        {
            var ratings = _ratingRepository.GetRatingByBook(bookId,page);
            double average = 0;
            foreach (var rating in ratings) average += rating.Rate;
            average /= ratings.Count;
            return new ResponseDTO()
            {
                Data = new { 
                    Ratings = _mapper.Map<List<RatingDTO>>(ratings),
                    Average = average
                },
                Total = _ratingRepository.GetRatingCount()
            };
        }

        public ResponseDTO GetRatings(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var ratings = _ratingRepository.GetRatings(page, pageSize, key, sortBy);
            return new ResponseDTO()
            {
                Data = _mapper.Map<List<RatingDTO>>(ratings),
                Total = _ratingRepository.GetRatingCount()
            };
        }

        //public ResponseDTO UpdateRating(int id, string name)
        //{
        //    var rating = _ratingRepository.GetRatingById(id);
        //    if (rating == null)
        //        return new ResponseDTO()
        //        {
        //            Code = 400,
        //            Message = "Rating không tồn tại"
        //        };
        //    rating.Update = DateTime.Now;
        //    rating.Rate = 0;
        //    _ratingRepository.UpdateRating(rating);
        //    if (_ratingRepository.IsSaveChanges()) return new ResponseDTO() { Message = "Cập nhật thành công" };
        //    else return new ResponseDTO() { Code = 400, Message = "Cập nhật thất bại" };
        //}
    }
}
