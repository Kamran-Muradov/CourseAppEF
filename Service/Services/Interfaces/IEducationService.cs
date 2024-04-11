using Domain.Models;
using Service.DTOs.Educations;

namespace Service.Services.Interfaces
{
    public interface IEducationService
    {
        Task CreateAsync(Education  data);
        Task UpdateAsync(Education data);
        Task DeleteAsync(int? id);
        Task<List<EducationDTo>> GetAllAsync();
        Task<List<EducationWithGroupsDTo>> GetAllWithGroupsAsync();
        Task<List<EducationDTo>> SortWithCreatedDateAsync(string sortCondition);
        Task<List<EducationDTo>> SearchByNameAsync(string searchText);
        Task<EducationDTo> GetByIdAsync(int? id);
    }
}
