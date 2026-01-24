using LibraryManagementApi.Data;
using LibraryManagementApi.Interfaces;
using LibraryManagementApi.Models.AuthorModels;
using LibraryManagementApi.Models.BookModels;
using LibraryManagementApi.Models.CategoryModels;
using LibraryManagementApi.Models.OrderItemsModels;
using LibraryManagementApi.Models.OrderModels;
using LibraryManagementApi.Models.ProductModels;
using LibraryManagementApi.Models.UserModels;
using LibraryManagementApi.Repository;
using LibraryManagementApi.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LibraryManagementDbContext>(q => q.UseSqlServer(builder.Configuration.GetConnectionString("LibraryManagementConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<
    IGenericRepository<AuthorModel, AuthorReadDto, AuthorUpdateDto, AuthorCreateDto>,
    AuthorRepository
>();
builder.Services.AddScoped<
    IGenericRepository<BookModel, BookReadDto, BookUpdateDto, BookCreateDto>,
    BookRepository 
>();
builder.Services.AddScoped<
    IGenericRepository<ProductModel, ProductReadDto, ProductUpdateDto, ProductCreateDto>,
    ProductRepository
>();
builder.Services.AddScoped<
    IGenericRepository<UserModel, UserReadDto, UserUpdateDto,UserCreateDto>,
    UserRepository
>();
builder.Services.AddScoped<
    IGenericRepository<OrderModel, OrderReadDto, OrderUpdateDto, OrderCreateDto>,
    OrderRepository
>();
builder.Services.AddScoped<
    IGenericRepository<OrderItemModel, OrderItemReadDto, OrderItemUpdateDto, OrderItemCreateDto>,
    OrderItemRepository
>();
builder.Services.AddScoped<
    IGenericRepository<CategoryModel, CategoryReadDto, CategoryUpdateDto, CategoryCreateDto>,
    CategoryRepository
>();
builder.Services.AddScoped<PriceFluctutaionService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
