using AutoMapper;
using LibraryManagementApi.Models.AuthorModels;
using LibraryManagementApi.Models.BookModels;
using LibraryManagementApi.Models.CategoryModels;
using LibraryManagementApi.Models.OrderItemsModels;
using LibraryManagementApi.Models.OrderModels;
using LibraryManagementApi.Models.ProductModels;
using LibraryManagementApi.Models.UserModels;

namespace LibraryManagementApi.Configurations
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            //book
            CreateMap<BookModel, BookReadDto>()
                 .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            CreateMap<BookCreateDto, BookModel>();
            CreateMap<BookUpdateDto, BookModel>();
            //Author
            CreateMap<AuthorModel, AuthorReadDto>();
            CreateMap<AuthorCreateDto, AuthorModel>();
            CreateMap<AuthorUpdateDto, AuthorModel>();
            //User
            CreateMap<UserModel, UserReadDto>()
                 .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders));
            CreateMap<UserCreateDto, UserModel>();
            CreateMap<UserUpdateDto, UserModel>();
            //Product
            CreateMap<ProductModel, ProductReadDto>()
                .ForMember(dest => dest.CategoryNames, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)))
                .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(src => src.Categories.Select(c => c.Id)))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ApplicablePrice));
            CreateMap<ProductUpdateDto, ProductModel>();
            CreateMap<ProductCreateDto, ProductModel>();
            //Order
            CreateMap<OrderModel, OrderReadDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));
            CreateMap<OrderCreateDto, OrderModel>()
             .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending"))
             .ForMember(dest => dest.TotalPrice, opt => opt.Ignore()); // بعداً محاسبه می‌شود
            CreateMap<OrderUpdateDto, OrderModel>();
            //OrderItems
            CreateMap<OrderItemModel, OrderItemReadDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            CreateMap<OrderItemCreateDto, OrderItemModel>()
            .ForMember(dest => dest.FeePrice, opt => opt.Ignore())   // محاسبه می‌شود
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore());     // ست می‌شود بعداً

            CreateMap<OrderItemUpdateDto, OrderItemModel>();
            //Category
            CreateMap<CategoryModel, CategoryReadDto>();
            CreateMap<CategoryCreateDto, CategoryModel>();
            CreateMap<CategoryUpdateDto, CategoryModel>();
            //OrderInUser
            CreateMap<OrderModel, OrderInUser>();
        }
    }
}
