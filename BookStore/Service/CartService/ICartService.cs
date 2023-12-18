using BookStore.DTOs.Cart;
using BookStore.DTOs.Response;

namespace BookStore.Service.CartService
{
    public interface ICartService
    {
        ResponseDTO GetCartByUser(int  userId);
        ResponseDTO AddToCart(int  userId, int bookId, int count);
    }
}
