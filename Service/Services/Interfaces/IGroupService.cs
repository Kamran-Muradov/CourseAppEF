using Domain.Models;
using Service.DTOs.Groups;

namespace Service.Services.Interfaces
{
    public interface IGroupService
    {
        Task CreateAsync(Group data);
        Task UpdateAsync(Group data);
        Task DeleteAsync(int? id);
        Task<List<GroupDTo>> GetAllAsync();
        Task<List<GroupDTo>> GetAllWithEducationIdAsync(int? id);
        Task<List<GroupDTo>> FilterByEducationNameAsync(string name);
        Task<List<GroupDTo>> SearchByNameAsync(string searchText);
        Task<List<GroupDTo>> SortWithCapacityAsync(string sortCondition);
        Task<GroupDTo> GetByIdAsync(int? id);
    }
}
