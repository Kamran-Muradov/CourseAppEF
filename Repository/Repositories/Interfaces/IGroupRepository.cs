using Domain.Models;

namespace Repository.Repositories.Interfaces
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        Task<List<Group>> GetAllWithEducationIdAsync(int? id);
        Task<List<Group>> FilterByEducationNameAsync(string name);
        Task<List<Group>> SortWithCapacityAsync(string sortCondition);
        Task<List<Group>> SearchByNameAsync(string searchText);
    }
}
