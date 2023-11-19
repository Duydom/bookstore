using BookStore.Model;
using BookStore.Profiles;
using BookStore.Repositories.AddressRepository;
using BookStore.Repositories.AuthorRepository;
using BookStore.Repositories.BookRepository;
using BookStore.Repositories.CartRepository;
using BookStore.Repositories.OrderRepository;
using BookStore.Repositories.PublisherRepository;
using BookStore.Repositories.QuantityRepository;
using BookStore.Repositories.RatingRepository;
using BookStore.Repositories.ShippingModeRepository;
using BookStore.Repositories.TagRepository;
using BookStore.Repositories.UserRepository;
using BookStore.Service.AddressService;
using BookStore.Service.AuthorService;
using BookStore.Service.AuthService;
using BookStore.Service.BookService;
using BookStore.Service.CartService;
using BookStore.Service.OrderService;
using BookStore.Service.PublisherService;
using BookStore.Service.RatingService;
using BookStore.Service.ShippingModeService;
using BookStore.Service.StatisticalService;
using BookStore.Service.TagService;
using BookStore.Service.UserService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("Default");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IAuthorRepository, AuthorRepository>();
services.AddScoped<IPublisherRepository, PublisherRepository>();
services.AddScoped<IAddressRepository, AddressRepository>();
services.AddScoped<ITagRepository, TagRepository>();
services.AddScoped<IShippingModeRepository, ShippingModeRepository>();
services.AddScoped<IBookRepository, BookRepository>();
services.AddScoped<IOrderRepository, OrderRepository>();
services.AddScoped<IQuantityRepository, QuantityRepository>();
services.AddScoped<ICartRepository, CartRepository>();
services.AddScoped<IRatingRepository, RatingRepository>();

services.AddScoped<IRatingService, RatingService>();
services.AddScoped<ICartService, CartService>();
services.AddScoped<IStatisticalService, StatisticalService>();
services.AddScoped<IOrderService, OrderService>();
services.AddScoped<IBookService, BookService>();
services.AddScoped<IShippingModeService, ShippingModeService>();
services.AddScoped<ITagService, TagService>();
services.AddScoped<IAddressService, AddressService>();
services.AddScoped<IPublisherService, PublisherService>();
services.AddScoped<IAuthorService, AuthorService>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<IAuthService, AuthService>();
//services.AddScoped<IUserService, UserService>();

services.AddAutoMapper(typeof(MapperProfile).Assembly);

services.AddCors(o =>
    o.AddPolicy("CorsPolicy", builder =>
        builder.WithOrigins("http://localhost:3000")
            .WithOrigins("http://localhost:3001")
            .AllowAnyHeader()
            .AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
