using AutoMapper;
using LibraryManagementApi.Data;
using LibraryManagementApi.Interfaces;
using LibraryManagementApi.Models.AuthorModels;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Repository
{
    public class AuthorRepository : IGenericRepository<AuthorModel, AuthorReadDto, AuthorUpdateDto, AuthorCreateDto>
    {
        private readonly LibraryManagementDbContext _context;
        private readonly IMapper _mapper;

        public AuthorRepository(IMapper mapper, LibraryManagementDbContext dbContext)
        {
            _context = dbContext;
            _mapper = mapper;
        }
        public async Task<AuthorReadDto> CreateAsync(AuthorCreateDto entity)
        {
            var Author = _mapper.Map<AuthorModel>(entity);
            await _context.Authors.AddAsync(Author);
            await _context.SaveChangesAsync();
            return _mapper.Map<AuthorReadDto>(Author);
        }

        public async Task DeleteAsync(int Id)
        {
            var entity = await _context.Authors.FirstOrDefaultAsync(x => x.Id == Id);
            if (entity == null)
                return;

            _context.Authors.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AuthorReadDto>> GetAllAsync()
        {
            var authors = await _context.Authors.ToListAsync();
            return _mapper.Map<List<AuthorReadDto>>(authors);
        }

        public async Task<bool> IsExist(int id)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id);
        }

        public async Task<AuthorReadDto> UpdateAsync(AuthorUpdateDto entity)
        {
            var author = await _context.Authors.FindAsync(entity.Id);
            if (author == null)
                return null;
            _mapper.Map(entity, author);

            await _context.SaveChangesAsync();

            return _mapper.Map<AuthorReadDto>(author);
        }
    }
}
