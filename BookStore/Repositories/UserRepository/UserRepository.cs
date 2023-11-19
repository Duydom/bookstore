using BookStore.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void CreateUser(User user)
        {
            _dataContext.Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            _dataContext.Entry(user).State = EntityState.Modified;
        }

        public User GetUserById(int id)
        {
            return _dataContext.Users.Include(r => r.Role).FirstOrDefault(u => u.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return _dataContext.Users.Include(r => r.Role).FirstOrDefault(u => u.Username == username);
        }

        public int GetUserCount()
        {
            return _dataContext.Users.Count();
        }

        public List<User> GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var query = _dataContext.Users.Where(r => r.RoleId == 2).AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(u => (u.FirstName.ToLower()+ " " + u.LastName.ToLower()).Contains(key.ToLower()));
            }

            switch (sortBy)
            {
                case "NAME":
                    query = query.OrderBy(u => u.LastName);
                    break;
                default:
                    query = query.OrderBy(u => u.IsDeleted).ThenByDescending(u => u.Create);
                    break;
            }
            if (page == null || pageSize == null || sortBy == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
        }

        public bool IsSaveChanges()
        {
            return _dataContext.SaveChanges() > 0;
        }

        public void UpdateUser(User user)
        {
            _dataContext.Entry(user).State = EntityState.Modified;
        }
    }
}
