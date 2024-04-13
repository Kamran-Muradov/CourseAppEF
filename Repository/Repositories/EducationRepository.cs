using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class EducationRepository : BaseRepository<Education>, IEducationRepository
    {
        public async Task<List<Education>> GetAllWithGroupsAsync()
        {
            return await Context.Set<Education>()
                .Include(m => m.Groups)
                .ToListAsync();
        }
    }
}
