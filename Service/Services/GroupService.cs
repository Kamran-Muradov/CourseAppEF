using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.Groups;
using Service.Helpers.Constants;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService()
        {
            _groupRepository = new GroupRepository();
        }

        public async Task CreateAsync(Group group)
        {
            ArgumentNullException.ThrowIfNull(group);

            await _groupRepository.CreateAsync(group);
        }

        public async Task UpdateAsync(Group group)
        {
            ArgumentNullException.ThrowIfNull(group);

            await _groupRepository.UpdateAsync(group);
        }

        public async Task DeleteAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(id);

            Group group = await _groupRepository.GetByIdAsync(id) ?? throw new NotFoundException(ResponseMessages.DataNotFound);

            await _groupRepository.DeleteAsync(group);
        }

        public async Task<List<GroupDTo>> GetAllAsync()
        {
            var datas = await _groupRepository.GetAllAsync();

            return datas.Select(m => new GroupDTo { Name = m.Name, Capacity = m.Capacity }).ToList();
        }

        public async Task<List<GroupDTo>> GetAllWithEducationIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(id);

            var datas = await _groupRepository.GetAllWithEducationIdAsync(id);

            return datas.Select(m => new GroupDTo { Name = m.Name, Capacity = m.Capacity }).ToList();
        }

        public async Task<List<GroupDTo>> FilterByEducationNameAsync(string name)
        {
            ArgumentNullException.ThrowIfNull(name);

            var datas = await _groupRepository.FilterByEducationNameAsync(name);

            return datas.Select(m => new GroupDTo { Name = m.Name, Capacity = m.Capacity }).ToList();
        }

        public async Task<List<GroupDTo>> SearchByNameAsync(string searchText)
        {
            ArgumentNullException.ThrowIfNull(searchText);

            var datas = await _groupRepository.GetAllAsync();

            var foundGroups = datas
                .Where(m => m.Name.ToLower().Contains(searchText.ToLower()))
                .Select(m => new GroupDTo { Name = m.Name, Capacity = m.Capacity })
                .ToList();

            return foundGroups;
        }

        public async Task<List<GroupDTo>> SortWithCapacityAsync(string sortCondition)
        {
            ArgumentNullException.ThrowIfNull(sortCondition);

            var datas = await _groupRepository.GetAllAsync();

            var groups = datas.Select(m => new GroupDTo { Name = m.Name, Capacity = m.Capacity })
                .ToList();

            switch (sortCondition)
            {
                case "asc":
                    return groups.OrderBy(m => m.Capacity).ToList();
                case "desc":
                    return groups.OrderByDescending(m => m.Capacity).ToList();
                default:
                    throw new FormatException(ResponseMessages.InvalidSortingFormat);
            }
        }

        public async Task<GroupDTo> GetByIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(id);

            var data = await _groupRepository.GetByIdAsync(id) ?? throw new NotFoundException(ResponseMessages.DataNotFound);

            return new GroupDTo { Name = data.Name, Capacity = data.Capacity };
        }
    }
}
