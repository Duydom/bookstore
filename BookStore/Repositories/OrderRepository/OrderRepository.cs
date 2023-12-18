using BookStore.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _dataContext;
        public OrderRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void CreateOrder(Order order)
        {
            _dataContext.Orders.Add(order);
        }

        public void DeleteOrder(Order order)
        {
            _dataContext.Orders.Remove(order);
        }

        public Order GetOrderById(int id)
        {
            return _dataContext.Orders.Include(o => o.User).Include(o => o.ShippingMode).Include(o => o.Address).Include(o => o.OrderBooks).ThenInclude(s => s.Book).FirstOrDefault(t => t.Id == id);
        }

        public List<Order> GetOrderByUser(int userId, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var query = _dataContext.Orders.Include(o => o.User).Include(o => o.ShippingMode).Include(o => o.Address).Include(o => o.OrderBooks).ThenInclude(s => s.Book).Where(o => o.User.Id == userId).AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(au => au.Description.ToLower().Contains(key.ToLower()));
            }

            switch (sortBy)
            {
                case "CREATE":
                    query = query.OrderBy(u => u.Create);
                    break;
                default:
                    query = query.OrderBy(u => u.IsDeleted).ThenBy(u => u.Id);
                    break;
            }
            if (page == null || pageSize == null || sortBy == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
        }

        public int GetOrderCount()
        {
            return _dataContext.Orders.Count();
        }

        public List<Order> GetOrders(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var query = _dataContext.Orders.Include(o => o.User).Include(o => o.ShippingMode).Include(o => o.Address).Include(o => o.OrderBooks).ThenInclude(s => s.Book).AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(au => au.Description.ToLower().Contains(key.ToLower()));
            }

            switch (sortBy)
            {
                case "CREATE":
                    query = query.OrderBy(u => u.Create);
                    break;
                default:
                    query = query.OrderBy(u => u.IsDeleted).ThenBy(u => u.Id);
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

        public void UpdateOrder(Order order)
        {
            _dataContext.Entry(order).State = EntityState.Modified;
        }
    }
}
