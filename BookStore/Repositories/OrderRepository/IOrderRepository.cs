using BookStore.Model;

namespace BookStore.Repositories.OrderRepository
{
    public interface IOrderRepository
    {

        List<Order> GetOrders(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        List<Order> GetOrderByUser(int userId, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID");
        Order GetOrderById(int id);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
        void CreateOrder(Order order);
        int GetOrderCount();
        bool IsSaveChanges();
    }
}
