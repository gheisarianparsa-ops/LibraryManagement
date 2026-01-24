using AutoMapper;
using LibraryManagementApi.Data;
using LibraryManagementApi.Interfaces;
using LibraryManagementApi.Models.PriceFluctuationsModel;
using LibraryManagementApi.Models.ProductModels;
using LibraryManagementApi.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Repository
{
    public class ProductRepository : IGenericRepository<ProductModel, ProductReadDto, ProductUpdateDto, ProductCreateDto>
    {
        private readonly LibraryManagementDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly PriceFluctutaionService _priceFluctService;
        public ProductRepository(IMapper mapper, LibraryManagementDbContext dbContext, PriceFluctutaionService priceFluctutaion)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _priceFluctService = priceFluctutaion;
        }
        public async Task<ProductReadDto> CreateAsync(ProductCreateDto dto)
        {
            var product = _mapper.Map<ProductModel>(dto);

            product.Categories = await _dbContext.Categories
                .Where(c => dto.CategoryIds.Contains(c.Id))
                .ToListAsync();

            product.PriceFluctuations.Add(new PriceFluctModel
            {
                Product=product,
                NewPrice=dto.Price,
                DeltaPrice=dto.Price,
                DateToApply=DateTimeOffset.Now
            });
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ProductReadDto>(product);
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
            var products = await _dbContext.Products
     .Include(p => p.Categories)
     .Include(p => p.PriceFluctuations)
     .ToListAsync();
            return _mapper.Map<List<ProductReadDto>>(products);
        }

        public async Task<ProductReadDto> GetById(int Id)
        {
            var product = await _dbContext.Products
                .Include(p => p.Categories)
                .Include(p => p.PriceFluctuations)
                .FirstOrDefaultAsync(p => p.Id == Id);

            if (product == null) return null;

            return _mapper.Map<ProductReadDto>(product);
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
            var OldPrice = Product.ApplicablePrice;
            ////test
            //entity.NewPriceDateToApply = DateTimeOffset.Now.AddMinutes(2);
           
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


            //در صورت تغییر قیمت
            if (entity.Price != OldPrice)
            {
                await _priceFluctService.PriceUpdator(new PriceFluctCreateDto
                {
                    ProductId = Product.Id,
                    OldPrice = OldPrice,
                    NewPrice = entity.Price,
                    DateToApply = (DateTimeOffset)entity.NewPriceDateToApply
                });
            }
            return _mapper.Map<ProductReadDto>(Product);
        }
    }
}
