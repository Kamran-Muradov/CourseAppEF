using Domain.Models;

namespace Repository.Repositories.Interfaces
{
    public interface IEducationRepository : IBaseRepository<Education>
    {
        Task<List<Education>> GetAllWithGroupsAsync();
    }
}
