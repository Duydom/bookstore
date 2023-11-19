using BookStore.Model;

namespace BookStore.Repositories.CartRepository
{
    public interface ICartRepository
    {
        List<Cart> GetCarts();
        void UpdateCart(Cart cart);
        void CreateCart(Cart cart);
        Cart GetCartById(int id);
        Cart GetCartByUser(int userId);
        bool IsSaveChanges();
    }
}
