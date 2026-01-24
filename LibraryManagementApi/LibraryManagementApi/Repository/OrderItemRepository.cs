using AutoMapper;
using LibraryManagementApi.Data;
using LibraryManagementApi.Interfaces;
using LibraryManagementApi.Models.OrderItemsModels;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Repository
{
    public class OrderItemRepository : IGenericRepository<OrderItemModel, OrderItemReadDto, OrderItemUpdateDto, OrderItemCreateDto>
    {
        private readonly LibraryManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderItemRepository(LibraryManagementDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OrderItemReadDto> CreateAsync(OrderItemCreateDto entity)
        {
            var orderItem = _mapper.Map<OrderItemModel>(entity);
            var product = await _dbContext.Products.Include(x => x.PriceFluctuations).SingleOrDefaultAsync(x => x.Id == entity.ProductId);
            var order = await _dbContext.Orders.FindAsync(entity.OrderId);

            if (product == null || order == null)
            {
                return null;
            }
            orderItem.Product = product;
            orderItem.Order = order;
            orderItem.FeePrice = product.ApplicablePrice;
            order.OrderItems.Add(orderItem);
            order.TotalPrice += orderItem.TotalPrice;
            await _dbContext.OrderItems.AddAsync(orderItem);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<OrderItemReadDto>(orderItem);
        }

        public async Task DeleteAsync(int Id)
        {
            var orderItem = await _dbContext.OrderItems.FirstOrDefaultAsync(oi => oi.Id == Id);
            if (orderItem == null) return;

            _dbContext.OrderItems.Remove(orderItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<OrderItemReadDto>> GetAllAsync()
        {
            var items = await _dbContext.OrderItems
                                        .Include(oi => oi.Product)
                                        .Include(oi => oi.Order)
                                        .ToListAsync();

            return _mapper.Map<List<OrderItemReadDto>>(items);
        }

        public async Task<OrderItemReadDto> GetById(int Id)
        {
            var item = await _dbContext.OrderItems
                                       .Include(oi => oi.Product)
                                       .Include(oi => oi.Order)
                                       .FirstOrDefaultAsync(oi => oi.Id == Id);

            return _mapper.Map<OrderItemReadDto>(item);
        }

        public async Task<bool> IsExist(int Id)
        {
            return await _dbContext.OrderItems.AnyAsync(oi => oi.Id == Id);
        }

        public async Task<OrderItemReadDto> UpdateAsync(int id, OrderItemUpdateDto entity)
        {
            var orderItem = await _dbContext.OrderItems
                                      .Include(oi => oi.Product)
        .ThenInclude(p => p.PriceFluctuations)
    .FirstOrDefaultAsync(oi => oi.Id == id);

            if (orderItem == null) return null;

            // فقط تعداد را آپدیت می‌کنیم
            orderItem.Quantity = entity.Quantity;

            orderItem.FeePrice = orderItem.Product.ApplicablePrice;

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<OrderItemReadDto>(orderItem);
        }
    }
}
