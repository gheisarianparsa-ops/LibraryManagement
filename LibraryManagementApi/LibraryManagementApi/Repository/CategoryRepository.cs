using AutoMapper;
using LibraryManagementApi.Data;
using LibraryManagementApi.Interfaces;
using LibraryManagementApi.Models.CategoryModels;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Repository
{
    public class CategoryRepository : IGenericRepository<CategoryModel, CategoryReadDto, CategoryUpdateDto, CategoryCreateDto>
    {
        private readonly LibraryManagementDbContext _dbcontext;
        private readonly IMapper _mapper;
        public CategoryRepository(LibraryManagementDbContext dbContext, IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
        }
        public async Task<CategoryReadDto> CreateAsync(CategoryCreateDto entity)
        {
            var category = _mapper.Map<CategoryModel>(entity);
            await _dbcontext.Categories.AddAsync(category);
            await _dbcontext.SaveChangesAsync();
            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task DeleteAsync(int Id)
        {
            var Categories = await _dbcontext.Categories.FirstOrDefaultAsync(u => u.Id == Id);
            if (Categories is null)
            {
                return;
            }
            _dbcontext.Categories.Remove(Categories);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<List<CategoryReadDto>> GetAllAsync()
        {
            var Categories = await _dbcontext.Categories.ToListAsync();
            return _mapper.Map<List<CategoryReadDto>>(Categories);
        }

        public async Task<CategoryReadDto> GetById(int Id)
        {
            var category = await _dbcontext.Categories.Include(x => x.Products).FirstOrDefaultAsync(u => u.Id == Id);
            if (category == null)
            {
                return null;
            }
            return _mapper.Map<CategoryReadDto>(category);

        }

        public async Task<bool> IsExist(int id)
        {
            return await _dbcontext.Categories.AnyAsync(u => u.Id == id);
        }

        public async Task<CategoryReadDto> UpdateAsync(int id, CategoryUpdateDto entity)
        {
            var category = await _dbcontext.Categories.FirstOrDefaultAsync(u => u.Id == id);
            if (category is null)
            {
                return null;
            }
            _mapper.Map(entity, category);
            await _dbcontext.SaveChangesAsync();
            return _mapper.Map<CategoryReadDto>(category);
        }
    }
}
