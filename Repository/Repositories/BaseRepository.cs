using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public BaseRepository()
        {
            _context = new AppDbContext();
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        //public async Task<List<T>> GetAllWithExpression(Func<T, bool> predicate)
        //{
        //    var datas = await _context.Set<T>().ToListAsync();

        //    return datas.Where(predicate).ToList();
        //}

        public async Task<T> GetByIdAsync(int? id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
