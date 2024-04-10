using Domain.Models;
using Service.DTOs.Educations;

namespace Service.Services.Interfaces
{
    public interface IEducationService
    {
        Task CreateAsync(Education  education);
        Task UpdateAsync(Education education);
        Task DeleteAsync(int? id);
        Task<List<EducationDTo>> GetAllAsync();
        Task<List<EducationWithGroupsDTo>> GetAllWithGroupsAsync();
        Task<List<EducationDTo>> SortWithCreatedDate(string sortCondition);
        Task<EducationDTo> GetByIdAsync(int? id);
    }
}
