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

        public async Task<List<Education>> SortWithCreatedDateAsync(string sortCondition)
        {
            switch (sortCondition)
            {
                case "asc":
                    return await Context.Set<Education>().OrderBy(m=>m.CreatedDate).ToListAsync();
                case "desc":
                    return await Context.Set<Education>().OrderByDescending(m => m.CreatedDate).ToListAsync();
                default:
                    return null;
            }
        }

        public async Task<List<Education>> SearchByNameAsync(string searchText)
        {
            return await Context.Set<Education>().Where(m => m.Name.ToLower().Contains(searchText.ToLower()))
                .ToListAsync();
        }
    }
}
