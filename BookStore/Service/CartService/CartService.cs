using AutoMapper;
using BookStore.DTOs.Cart;
using BookStore.DTOs.Order;
using BookStore.DTOs.Response;
using BookStore.Model;
using BookStore.Repositories.BookRepository;
using BookStore.Repositories.CartRepository;
using BookStore.Repositories.QuantityRepository;
using BookStore.Repositories.TagRepository;
using BookStore.Repositories.UserRepository;

namespace BookStore.Service.CartService
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IQuantityRepository _quantityRepository;
        private readonly IMapper _mapper;
        public CartService(ICartRepository cartRepository, IMapper mapper, IUserRepository userRepository, IQuantityRepository quantityRepository, IBookRepository bookRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _quantityRepository = quantityRepository;
        }

        public ResponseDTO AddToCart(int userId, int bookId, int count)
        {
            var cart = _cartRepository.GetCartByUser(userId);
            if (cart == null) return new ResponseDTO { Code = 400, Message = "Cart của user không tồn tại" };

            bool yet = false;
            for (int i = 0; i < cart.Books.Count(); i++)
            {
                if (cart.Books[i].Id == bookId)
                {
                    yet = true;
                    if (count == 0)
                    {
                        break;
                    }
                    if(cart.Quantities[i].Count + count == 0)
                    {
                        cart.Books.RemoveAt(i);
                        cart.Quantities.RemoveAt(i);

                        break;
                    }
                    var quantity = _quantityRepository.GetQuantity(cart.Quantities[i].Count + count);
                    if (quantity != null)
                    {
                        cart.Quantities[i] = quantity;
                    }
                    else
                    {
                        _quantityRepository.CreateQuantity(cart.Quantities[i].Count + count);
                        if (_quantityRepository.IsSaveChanges())
                        {
                            quantity = _quantityRepository.GetQuantity(cart.Quantities[i].Count + count);
                            if (quantity != null)
                            {
                                cart.Quantities[i] = quantity;
                            }
                        }

                    }

                    break;
                }
            }
            if (!yet)
            {
                var book = _bookRepository.GetBookById(bookId);
                if (book != null)
                {
                    var quantity = _quantityRepository.GetQuantity(count);
                    if (quantity != null)
                    {
                        cart.Books.Add(book);
                        cart.Quantities.Add(quantity);
                    }
                    else
                    {
                        _quantityRepository.CreateQuantity(count);
                        if (_quantityRepository.IsSaveChanges())
                        {
                            quantity = _quantityRepository.GetQuantity(count);
                            if (quantity != null)
                            {
                                cart.Books.Add(book);
                                cart.Quantities.Add(quantity);
                            }
                        }

                    }
                }
            }

            cart.Update = DateTime.Now;
            _cartRepository.UpdateCart(cart);
            if (_cartRepository.IsSaveChanges())
                return new ResponseDTO()
                {
                    Message = "Cập nhật thành công"
                };
            else return new ResponseDTO()
            {
                Code = 400,
                Message = "Cập nhật thất bại"
            };
        }

        public ResponseDTO CreateCart(int userId, CreateCartDTO createCartDTO)
        {
            var cart = _cartRepository.GetCartByUser(userId);
            if (cart == null) return new ResponseDTO { Code = 400, Message = "Cart của user không tồn tại" };

            for (int i = 0; i < createCartDTO.BookIds.Count(); i++)
            {
                var book = _bookRepository.GetBookById(createCartDTO.BookIds[i]);
                if (book != null)
                {
                    var quantity = _quantityRepository.GetQuantity(createCartDTO.QuantitieCounts[i]);
                    if (quantity != null && quantity.Count != 0)
                    {
                        cart.Books.Add(book);
                        cart.Quantities.Add(quantity);
                    }
                    else
                    {
                        _quantityRepository.CreateQuantity(createCartDTO.QuantitieCounts[i]);
                        if (_quantityRepository.IsSaveChanges())
                        {
                            quantity = _quantityRepository.GetQuantity(createCartDTO.QuantitieCounts[i]);
                            if (quantity != null)
                            {
                                cart.Books.Add(book);
                                cart.Quantities.Add(quantity);
                            }
                        }
                    }
                }
            }

            cart.Update = DateTime.Now;

            _cartRepository.UpdateCart(cart);
            if (_cartRepository.IsSaveChanges())
                return new ResponseDTO()
                {
                    Message = "Tạo thành công"
                };
            else return new ResponseDTO()
            {
                Code = 400,
                Message = "Tạo thất bại"
            };
        }

        public ResponseDTO GetCartByUser(int userId)
        {
            var cart = _cartRepository.GetCartByUser(userId);
            if (cart != null)
                return new ResponseDTO
                {
                    Data = _mapper.Map<CartDTO>(cart)
                };
            else return new ResponseDTO
            {
                Code = 400,
                Message = "Giỏ hàng của user này không tồn tại"
            };
        }

        public ResponseDTO UpdateCart(int userId, int bookId, int count)
        {
            var cart = _cartRepository.GetCartByUser(userId);
            if (cart == null) return new ResponseDTO { Code = 400, Message = "Cart của user không tồn tại" };
            for (int i = 0; i < cart.Books.Count(); i++)
            {
                if (cart.Books[i].Id == bookId)
                {
                    if (count == 0)
                    {
                        cart.Books.RemoveAt(i);
                        cart.Quantities.RemoveAt(i);
                    }
                    var quantity = _quantityRepository.GetQuantity(count);
                    if (quantity != null)
                    {
                        cart.Quantities[i] = quantity;
                    }
                    else
                    {
                        _quantityRepository.CreateQuantity(count);
                        if (_quantityRepository.IsSaveChanges())
                        {
                            quantity = _quantityRepository.GetQuantity(count);
                            if (quantity != null)
                            {
                                cart.Quantities[i] = quantity;
                            }
                        }

                    }

                    break;
                }
            }

            cart.Update = DateTime.Now;
            _cartRepository.UpdateCart(cart);
            if (_cartRepository.IsSaveChanges())
                return new ResponseDTO()
                {
                    Message = "Cập nhật thành công"
                };
            else return new ResponseDTO()
            {
                Code = 400,
                Message = "Cập nhật thất bại"
            };
        }
    }
}
