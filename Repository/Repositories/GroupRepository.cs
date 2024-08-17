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

        public async Task<List<Group>> SortWithCapacityAsync(string sortCondition)
        {
            switch (sortCondition)
            {
                case "asc":
                    return await Context.Set<Group>().OrderBy(m => m.Capacity).ToListAsync();
                case "desc":
                    return await Context.Set<Group>().OrderByDescending(m => m.Capacity).ToListAsync();
                default:
                    return null;
            }
        }

        public async Task<List<Group>> SearchByNameAsync(string searchText)
        {
            return await Context.Set<Group>().Where(m => m.Name.ToLower().Contains(searchText.ToLower()))
                .ToListAsync();
        }


    }
}
