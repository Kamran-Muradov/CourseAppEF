using Domain.Models;

namespace Repository.Repositories.Interfaces
{
    public interface IEducationRepository : IBaseRepository<Education>
    {
        Task<List<Education>> GetAllWithGroupsAsync();
        Task<List<Education>> SortWithCreatedDateAsync(string sortCondition);
        Task<List<Education>> SearchByNameAsync(string searchText);
    }
}
