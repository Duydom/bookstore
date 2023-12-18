using AutoMapper;
using BookStore.DTOs;
using BookStore.DTOs.Address;
using BookStore.DTOs.Author;
using BookStore.DTOs.Book;
using BookStore.DTOs.Cart;
using BookStore.DTOs.CartBook;
using BookStore.DTOs.Order;
using BookStore.DTOs.OrderBook;
using BookStore.DTOs.Publisher;
using BookStore.DTOs.Rate;
using BookStore.DTOs.Role;
using BookStore.DTOs.ShippingMode;
using BookStore.DTOs.Tag;
using BookStore.DTOs.User;
using BookStore.Model;

namespace BookStore.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();

            CreateMap<RegisterUserDTO, User>();
            CreateMap<User, RegisterUserDTO>();

            CreateMap<CreateUserDTO, User>();
            CreateMap<User, CreateUserDTO>();

            CreateMap<UpdateUserDTO, User>();
            CreateMap<User, UpdateUserDTO>();

            CreateMap<AuthorDTO, Author>();
            CreateMap<Author, AuthorDTO>();

            CreateMap<PublisherDTO, Publisher>();
            CreateMap<Publisher, PublisherDTO>();

            CreateMap<AddressDTO, Address>();
            CreateMap<Address, AddressDTO>();

            CreateMap<CreateAddressDTO, Address>();
            CreateMap<Address, CreateAddressDTO>();

            CreateMap<UpdateAddressDTO, Address>();
            CreateMap<Address, UpdateAddressDTO>();

            CreateMap<TagDTO, Tag>();
            CreateMap<Tag, TagDTO>();

            CreateMap<ShippingModeDTO, ShippingMode>();
            CreateMap<ShippingMode, ShippingModeDTO>();

            CreateMap<BookDTO, Book>();
            CreateMap<Book, BookDTO>();

            CreateMap<CreateBookDTO, Book>();
            CreateMap<Book, CreateBookDTO>();

            CreateMap<UpdateBookDTO, Book>();
            CreateMap<Book, UpdateBookDTO>();

            CreateMap<QuantityDTO, Quantity>();
            CreateMap<Quantity, QuantityDTO>();

            CreateMap<OrderDTO, Order>();
            CreateMap<Order, OrderDTO>();

            CreateMap<CreateOrderDTO, Order>();
            CreateMap<Order, CreateOrderDTO>();

            CreateMap<UpdateOrderDTO, Order>();
            CreateMap<Order, UpdateOrderDTO>();

            CreateMap<CartDTO, Cart>();
            CreateMap<Cart, CartDTO>();

            CreateMap<RoleDTO, Role>();
            CreateMap<Role, RoleDTO>();

            CreateMap<RatingDTO, Rating>();
            CreateMap<Rating, RatingDTO>();

            CreateMap<CreateRatingDTO, Rating>();
            CreateMap<Rating, CreateRatingDTO>();

            CreateMap<CartBookDTO, CartBook>();
            CreateMap<CartBook, CartBookDTO>();

            CreateMap<OrderBook, OrderBookDTO>();
            CreateMap<OrderBookDTO, OrderBook>();

            //CreateMap<Book, CreateBookDTO>()
            //.ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));
            //
            //CreateMap<CreateBookDTO, Book>()
            //.ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));
        }
    }
}
