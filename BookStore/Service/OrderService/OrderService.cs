using AutoMapper;
using BookStore.DTOs.Order;
using BookStore.DTOs.Response;
using BookStore.Model;
using BookStore.Repositories.AddressRepository;
using BookStore.Repositories.BookRepository;
using BookStore.Repositories.CartRepository;
using BookStore.Repositories.OrderRepository;
using BookStore.Repositories.QuantityRepository;
using BookStore.Repositories.ShippingModeRepository;
using BookStore.Repositories.UserRepository;

namespace BookStore.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IShippingModeRepository _shippingModeRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IQuantityRepository _quantityRepository;
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IBookRepository bookRepository,
            IUserRepository userRepository, IShippingModeRepository shippingModeRepository,
            IAddressRepository addressRepository, IQuantityRepository quantityRepository, ICartRepository cartRepository
            )
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _shippingModeRepository = shippingModeRepository;
            _quantityRepository = quantityRepository;
            _addressRepository = addressRepository;
            _cartRepository = cartRepository;
        }

        public ResponseDTO CreateOrder(CreateOrderDTO createOrderDTO)
        {
            var user = _userRepository.GetUserById(createOrderDTO.UserId);
            if (user == null) return new ResponseDTO()
            {
                Code = 400,
                Message = "User không tồn tại"
            };

            var cart = _cartRepository.GetCartByUser(user.Id);

            var shippingMode = _shippingModeRepository.GetShippingModeById(createOrderDTO.ShippingModeId);
            if (shippingMode == null) return new ResponseDTO()
            {
                Code = 400,
                Message = "ShippingMode không tồn tại"
            };
            var address = _addressRepository.GetAddressById(createOrderDTO.AddressId);
            if (address == null) return new ResponseDTO()
            {
                Code = 400,
                Message = "Address không tồn tại"
            };

            if (user.Addresses.IndexOf(address) < 0)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Địa chỉ không hợp lệ"
                };
            }

            var order = _mapper.Map<Order>(createOrderDTO);

            for (int i = 0; i < createOrderDTO.BookIds.Count(); i++)
            {
                var book = _bookRepository.GetBookById(createOrderDTO.BookIds[i]);
                if (book != null)
                {
                    int index = cart.Books.FindIndex(o => o.Id == book.Id);
                    if (index >= 0 && cart.Quantities[index].Count == createOrderDTO.QuantitieCounts[i])
                    {
                        cart.Books.RemoveAt(index);
                        cart.Quantities.RemoveAt(index);
                    }

                    var quantity = _quantityRepository.GetQuantity(createOrderDTO.QuantitieCounts[i]);
                    if (quantity != null)
                    {
                        order.Books.Add(book);
                        order.Quantities.Add(quantity);
                    }
                    else
                    {
                        _quantityRepository.CreateQuantity(createOrderDTO.QuantitieCounts[i]);
                        if (_quantityRepository.IsSaveChanges())
                        {
                            quantity = _quantityRepository.GetQuantity(createOrderDTO.QuantitieCounts[i]);
                            if (quantity != null)
                            {
                                order.Books.Add(book);
                                order.Quantities.Add(quantity);
                            }
                        }

                    }
                }
            }

            if (order.Books.Count == 0) return new ResponseDTO()
            {
                Code = 400,
                Message = "Book không được để trống"
            };

            _cartRepository.UpdateCart(cart);
            if (_cartRepository.IsSaveChanges() == false) return new ResponseDTO()
            {
                Code = 400,
                Message = "Tạo thất bại"
            };

            _orderRepository.CreateOrder(order);
            if (_orderRepository.IsSaveChanges())
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

        public ResponseDTO DeleteOrder(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null)
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Order không tồn tại"
                };

            order.IsDeleted = true;
            _orderRepository.UpdateOrder(order);
            if (_orderRepository.IsSaveChanges())
                return new ResponseDTO()
                {
                    Message = "Xóa thành công"
                };
            else return new ResponseDTO()
            {
                Code = 400,
                Message = "Xóa thất bại"
            };
        }

        public ResponseDTO GetOrderByUser(int userId, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null) return new ResponseDTO()
            {
                Code = 400,
                Message = "User không tồn tại"
            };

            var orders = _orderRepository.GetOrderByUser(userId, page, pageSize, key, sortBy);
            return new ResponseDTO()
            {
                Code = 200,
                Data = _mapper.Map<List<OrderDTO>>(orders),
            };
        }

        public ResponseDTO GetOrderById(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null)
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Order không tồn tại"
                };

            return new ResponseDTO()
            {
                Data = _mapper.Map<OrderDTO>(order)
            };
        }

        public ResponseDTO GetOrders(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "ID")
        {
            var orders = _orderRepository.GetOrders(page, pageSize, key, sortBy);
            return new ResponseDTO()
            {
                Data = _mapper.Map<List<OrderDTO>>(orders),
                Total = _orderRepository.GetOrderCount()
            };
        }

        public ResponseDTO UpdateOrder(int id, UpdateOrderDTO updateOrderDTO)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null)
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "Order không tồn tại"
                };

            order.Update = DateTime.Now;
            order.Status = updateOrderDTO.Status;
            order.Description = updateOrderDTO.Description;
            order.ShippingModeId = updateOrderDTO.ShippingModeId;

            _orderRepository.UpdateOrder(order);
            if (_orderRepository.IsSaveChanges())
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
