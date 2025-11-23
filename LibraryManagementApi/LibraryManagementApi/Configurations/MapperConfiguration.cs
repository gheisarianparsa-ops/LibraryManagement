using AutoMapper;
using LibraryManagementApi.Models.AuthorModels;
using LibraryManagementApi.Models.BookModels;

namespace LibraryManagementApi.Configurations
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<BookModel, BookReadDto>()
                 .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));
            CreateMap<BookCreateDto, BookModel>();
            CreateMap<BookUpdateDto, BookModel>();
            CreateMap<AuthorModel, AuthorReadDto>();
            CreateMap<AuthorCreateDto, AuthorModel>();
            CreateMap<AuthorUpdateDto, AuthorModel>();
        }
    }
}
