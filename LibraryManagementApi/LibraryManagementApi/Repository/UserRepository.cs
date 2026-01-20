using AutoMapper;
using LibraryManagementApi.Data;
using LibraryManagementApi.Interfaces;
using LibraryManagementApi.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Repository
{
    public class UserRepository : IGenericRepository<UserModel, UserReadDto, UserUpdateDto, UserCreateDto>
    {
        private readonly LibraryManagementDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserRepository(IMapper mapper, LibraryManagementDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<UserReadDto> CreateAsync(UserCreateDto entity)
        {
            var user = _mapper.Map<UserModel>(entity);
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<UserReadDto>(entity);
        }

        public async Task DeleteAsync(int Id)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if (entity is null)
            {
                return;
            }
            _dbContext.Users.Remove(entity);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<List<UserReadDto>> GetAllAsync()
        {
            var users = await _dbContext.Users.ToListAsync();
            return _mapper.Map<List<UserReadDto>>(users);
        }

        public async Task<UserReadDto> GetById(int Id)
        {
            var user = await _dbContext.Users.Include(x => x.Orders).SingleOrDefaultAsync(x => x.Id == Id);
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<bool> IsExist(int id)
        {
            return await _dbContext.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<UserReadDto> UpdateAsync(int Id,UserUpdateDto entity)
        {
            var user = _dbContext.Users.FirstOrDefaultAsync(u => u.Id == Id);
            if (user is null)
            {
                return null;
            }
            await _mapper.Map(entity, user);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<UserReadDto>(user);
        }
    }
}
