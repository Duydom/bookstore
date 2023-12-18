using BookStore.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.CartRepository
{
    public class CartRepository : ICartRepository
    {
        private readonly DataContext _dataContext;
        public CartRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void CreateCart(Cart cart)
        {
            _dataContext.Carts.Add(cart);
        }

        public Cart GetCartById(int id)
        {
            return _dataContext.Carts.FirstOrDefault(t => t.Id == id);
        }
        public Cart GetCartByUser(int userId)
        {
            var user = _dataContext.Users.Include(c => c.Cart).FirstOrDefault(c => c.Id == userId);

            if (user == null) return null;

            var cart = _dataContext.Carts.Include(c => c.CartBooks).ThenInclude(c => c.Book).FirstOrDefault(c => c.Id == user.Cart.Id);
            return cart;
        }

        public List<Cart> GetCarts()
        {
            throw new NotImplementedException();
        }

        public bool IsSaveChanges()
        {
            return _dataContext.SaveChanges() > 0;
        }

        public void UpdateCart(Cart cart)
        {
            _dataContext.Entry(cart).State = EntityState.Modified;
        }
    }
}
