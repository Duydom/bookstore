using BookStore.DTOs.Cart;
using BookStore.DTOs.Response;

namespace BookStore.Service.CartService
{
    public interface ICartService
    {
        ResponseDTO GetCartByUser(int  userId);
        ResponseDTO UpdateCart(int  userId, int bookId, int count);
        ResponseDTO CreateCart(int  userId, CreateCartDTO createCartDTO);
        ResponseDTO AddToCart(int  userId, int bookId, int count);
    }
}
