using Service.DTOs.Groups;

namespace Service.Services.Interfaces
{
    public interface IGroupService
    {
        Task CreateAsync(GroupCreateDTo data);
        Task UpdateAsync(GroupUpdateDTo data);
        Task DeleteAsync(int? id);
        Task<List<GroupDTo>> GetAllAsync();
        Task<List<GroupDTo>> GetAllWithEducationIdAsync(int? id);
        Task<List<GroupDTo>> FilterByEducationNameAsync(string name);
        Task<List<GroupDTo>> SortWithCapacityAsync(string sortCondition);
        Task<List<GroupDTo>> SearchByNameAsync(string searchText);
        Task<GroupDTo> GetByIdAsync(int? id);
    }
}
