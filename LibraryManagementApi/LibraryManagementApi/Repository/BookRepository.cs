using AutoMapper;
using LibraryManagementApi.Data;
using LibraryManagementApi.Interfaces;
using LibraryManagementApi.Models.BookModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Repository
{
    public class BookRepository : IGenericRepository<BookModel, BookReadDto, BookUpdateDto, BookCreateDto>
    {
        private readonly LibraryManagementDbContext _context;
        private readonly IMapper _mapper;
        public BookRepository(IMapper mapper, LibraryManagementDbContext dbContext)
        {
            _context = dbContext;
            _mapper = mapper;   
        }
        public async Task<BookReadDto> CreateAsync(BookCreateDto entity)
        {
            var book = _mapper.Map<BookModel>(entity);
           await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return _mapper.Map<BookReadDto>(book);
        }

        public async Task DeleteAsync(int Id)
        {
            var book =await _context.Books.FirstOrDefaultAsync(q => q.Id == Id);
            if (book != null) 
            {
                 _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task<List<BookReadDto>> GetAllAsync()
        {
            var books=await _context.Books.ToListAsync();
            return _mapper.Map<List<BookReadDto>>(books);
        }

        public async Task<bool> IsExist(int id)
        {
            return await _context.Books.AnyAsync(a => a.Id == id);
        }

        public async Task<BookReadDto> UpdateAsync(BookUpdateDto entity)
        {
            var book = await _context.Books.FindAsync(entity.Id);
            if (book == null)
                return null; 

            _mapper.Map(entity, book);

            await _context.SaveChangesAsync();
            return _mapper.Map<BookReadDto>(book);
        }
    }
}
