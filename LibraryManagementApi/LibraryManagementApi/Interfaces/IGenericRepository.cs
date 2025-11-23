namespace LibraryManagementApi.Interfaces
{
    public interface IGenericRepository<TModel , TRead,TUpdate, TCreate> where TModel : class
    {
        Task<List<TRead>> GetAllAsync();
        Task<TRead> CreateAsync(TCreate entity);
        Task<TRead> UpdateAsync(TUpdate entity);
        Task DeleteAsync(int Id);
        Task<bool> IsExist(int id);
    }
}
