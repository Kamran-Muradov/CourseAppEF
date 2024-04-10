using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class EducationRepository : BaseRepository<Education>, IEducationRepository
    {
        private readonly AppDbContext _context;

        public EducationRepository()
        {
            _context = new AppDbContext();
        }

        public async Task<List<Education>> GetAllWithGroupsAsync()
        {
            return await _context.Set<Education>()
                .Include(m => m.Groups)
                .ToListAsync();
        }
    }
}
