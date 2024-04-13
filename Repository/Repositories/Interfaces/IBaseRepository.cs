using Domain.Common;

namespace Repository.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task<List<T>> GetAllAsync();
        IQueryable<T> GetALl();
        Task<T> GetByIdAsync(int? id);

    }
}
