using Domain.Models;
using Service.DTOs.Educations;

namespace Service.Services.Interfaces
{
    public interface IGroupService
    {
        Task CreateAsync(Group group);
        Task UpdateAsync(Group group);
        Task DeleteAsync(int? id);
        Task<List<EducationDTo>> GetAllAsync();
        Task<EducationDTo> GetByIdAsync(int? id);

    }
}
