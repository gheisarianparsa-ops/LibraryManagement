using AutoMapper;
using LibraryManagementApi.Data;
using LibraryManagementApi.Interfaces;
using LibraryManagementApi.Models.ProductModels;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Repository
{
    public class ProductRepository : IGenericRepository<ProductModel, ProductReadDto, ProductUpdateDto, ProductCreateDto>
    {
        private readonly LibraryManagementDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProductRepository(IMapper mapper, LibraryManagementDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ProductReadDto> CreateAsync(ProductCreateDto entity)
        {
            var Product = _mapper.Map<ProductModel>(entity);
            await _dbContext.Products.AddAsync(Product);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ProductReadDto>(Product);
        }

        public async Task DeleteAsync(int Id)
        {
            var Product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == Id);
            if (Product is null)
            {
                return;
            }
            _dbContext.Products.Remove(Product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ProductReadDto>> GetAllAsync()
        {
            var Products = await _dbContext.Products.ToListAsync();
            return _mapper.Map<List<ProductReadDto>>(Products);
        }

        public async Task<ProductReadDto> GetById(int Id)
        {
            var Product = await _dbContext.Products.Include(x => x.Categories).FirstAsync(p => p.Id == Id);
            if (Product == null)
            {
                return null;
            }
            return _mapper.Map<ProductReadDto>(Product);
        }

        public async Task<bool> IsExist(int id)
        {
            return await _dbContext.Products.AnyAsync(p => p.Id == id);
        }

        public async Task<ProductReadDto> UpdateAsync(int Id, ProductUpdateDto entity)
        {
            var Product = await _dbContext.Products.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == Id);
            if (Product == null)
            {
                return null;
            }
            _mapper.Map(entity, Product);
            if (entity.CategoryIds != null)
            {
                var Categories = await _dbContext.Categories.Where(x => entity.CategoryIds.Contains(x.Id)).ToListAsync();
                Product.Categories.Clear();
                foreach (var category in Categories)
                {
                    Product.Categories.Add(category);
                }
                await _dbContext.SaveChangesAsync();
            }
            return _mapper.Map<ProductReadDto>(Product);

        }
    }
}
