using LibraryManagementApi.Models.AuthorModels;
using LibraryManagementApi.Models.BookModels;
using LibraryManagementApi.Models.CategoryModels;
using LibraryManagementApi.Models.OrderItemsModels;
using LibraryManagementApi.Models.OrderModels;
using LibraryManagementApi.Models.PriceFluctuationsModel;
using LibraryManagementApi.Models.ProductModels;
using LibraryManagementApi.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace LibraryManagementApi.Data
{
    public class LibraryManagementDbContext : DbContext
    {
        public LibraryManagementDbContext(DbContextOptions<LibraryManagementDbContext> options) : base(options)
        {
        }
        public DbSet<BookModel> Books { get; set; }
        public DbSet<AuthorModel> Authors { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderItemModel> OrderItems { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<PriceFluctModel> PriceFlucts { get; set; }
       
    }

}
