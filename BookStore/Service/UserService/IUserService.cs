using BookStore.DTOs.Response;
using BookStore.DTOs.User;
using BookStore.Model;

namespace BookStore.Service.UserService
{
    public interface IUserService
    {
        ResponseDTO GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        ResponseDTO GetUserById(int id);
        ResponseDTO GetUserByUsername(string username);
        ResponseDTO UpdateUser(int id, UpdateUserDTO updateUserDTO);
        ResponseDTO DeleteUser(int id);
        ResponseDTO CreateUser(CreateUserDTO createUserDTO);
    }
}
