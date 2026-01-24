using AutoMapper;
using LibraryManagementApi.Data;
using LibraryManagementApi.Interfaces;
using LibraryManagementApi.Models.OrderModels;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Repository
{
    public class OrderRepository : IGenericRepository<OrderModel, OrderReadDto, OrderUpdateDto, OrderCreateDto>
    {
        private readonly LibraryManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderRepository(LibraryManagementDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OrderReadDto> CreateAsync(OrderCreateDto entity)
        {
            //User Validation
            var user = await _dbContext.Users.FindAsync(entity.UserId);
            if (user == null)
            {
                throw new Exception("User Not Found");
            }
            //Create Order
            var order = _mapper.Map<OrderModel>(entity);
            order.User = user;
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<OrderReadDto>(order);
        }

        public async Task DeleteAsync(int Id)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == Id);
            if (order == null) return;

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<OrderReadDto>> GetAllAsync()
        {
            var orders = await _dbContext.Orders
                                         .Include(o => o.OrderItems)
                                             .ThenInclude(oi => oi.Product)
                                         .Include(o => o.User)
                                         .ToListAsync();

            return _mapper.Map<List<OrderReadDto>>(orders);
        }

        public async Task<OrderReadDto> GetById(int Id)
        {
            var order = await _dbContext.Orders
                                        .Include(o => o.OrderItems)
                                            .ThenInclude(oi => oi.Product)
                                        .Include(o => o.User)
                                        .FirstOrDefaultAsync(o => o.Id == Id);

            return _mapper.Map<OrderReadDto>(order);
        }

        public async Task<bool> IsExist(int id)
        {
            return await _dbContext.Orders.AnyAsync(o => o.Id == id);
        }

        public async Task<OrderReadDto> UpdateAsync(int Id, OrderUpdateDto entity)
        {
            var order = await _dbContext.Orders
                                        .Include(o => o.OrderItems)
                                        .FirstOrDefaultAsync(o => o.Id == Id);

            if (order == null) return null;

            _mapper.Map(entity, order);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<OrderReadDto>(order);
        }
    }
}
