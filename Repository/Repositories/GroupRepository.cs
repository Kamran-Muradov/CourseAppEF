using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public async Task<List<Group>> GetAllWithEducationIdAsync(int? id)
        {
            return await Context.Set<Group>().Where(m => m.EducationId == id).ToListAsync();
        }

        public async Task<List<Group>> FilterByEducationNameAsync(string name)
        {
            return await Context.Set<Group>()
                .Include(m => m.Education)
                .Where(m => m.Education.Name.ToLower() == name.ToLower())
                .ToListAsync();
        }
    }
}
