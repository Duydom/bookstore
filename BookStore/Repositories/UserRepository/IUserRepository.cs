using BookStore.Model;

namespace BookStore.Repositories.UserRepository
{
    public interface IUserRepository
    {
        List<User> GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        User GetUserById(int id);
        User GetUserByUsername(string username);
        void UpdateUser(User user);
        void DeleteUser(User user);
        void CreateUser(User user);
        int GetUserCount();
        bool IsSaveChanges();
    }
}
