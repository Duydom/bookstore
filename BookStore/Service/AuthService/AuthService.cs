using AutoMapper;
using BookStore.DTOs.Response;
using BookStore.DTOs.User;
using BookStore.Model;
using BookStore.Repositories.CartRepository;
using BookStore.Repositories.UserRepository;

namespace BookStore.Service.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        public AuthService(IUserRepository userRepository, ICartRepository cartRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _mapper = mapper;
        }
        public ResponseDTO Login(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null)
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Username không tồn tại"
                };
            if (user.Password != password)
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Mật khẩu không chính xác"
                };
            return new ResponseDTO()
            {
                Message = "Login thành công",
                Data = _mapper.Map<UserDTO>(user)
            };
        }

        public ResponseDTO Register(RegisterUserDTO registerUserDTO)
        {
            var user = _userRepository.GetUserByUsername(registerUserDTO.Username);
            if (user != null)
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Username đã tồn tại"
                };
            if (registerUserDTO.Password != registerUserDTO.CfPassword)
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Password không trùng khớp"
                };
            user = _mapper.Map<User>(registerUserDTO);
            user.RoleId = 2;
            _userRepository.CreateUser(user);
            if (_userRepository.IsSaveChanges())
            {
                return new ResponseDTO()
                {
                    Message = "Tạo thành công"
                };
            }
            else return new ResponseDTO()
            {
                Code = 400,
                Message = "Tạo thất bại"
            };
        }
    }
}
