using BookStore.DTOs.Response;
using BookStore.DTOs.User;

namespace BookStore.Service.AuthService
{
    public interface IAuthService
    {
        ResponseDTO Login(string username, string password);
        ResponseDTO Register(RegisterUserDTO registerUserDTO);
    }
}
