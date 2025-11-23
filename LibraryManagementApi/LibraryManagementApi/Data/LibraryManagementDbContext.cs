using LibraryManagementApi.Models.AuthorModels;
using LibraryManagementApi.Models.BookModels;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Data
{
    public class LibraryManagementDbContext:DbContext
    {
        public LibraryManagementDbContext(DbContextOptions options) :base(options)
        {
        }
        public DbSet<BookModel> Books { get; set; }
        public DbSet<AuthorModel> Authors { get; set; }

    }
}
