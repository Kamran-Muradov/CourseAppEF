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

        public async Task CreateAsync(GroupCreateDTo data)
        {
            ArgumentNullException.ThrowIfNull(data);

            await _groupRepository.CreateAsync(new Group
            {
                Name = data.Name,
                Capacity = data.Capacity,
                EducationId = data.EducationId,
                CreatedDate = DateTime.Now
            });
        }

        public async Task UpdateAsync(GroupUpdateDTo data)
        {
            ArgumentNullException.ThrowIfNull(data);

            Group group = await _groupRepository.GetByIdAsync(data.Id) ?? throw new NotFoundException(ResponseMessages.DataNotFound);

            if (!string.IsNullOrEmpty(data.Name))
            {
                group.Name = data.Name;
            }

            if (data.Capacity > 0)
            {
                group.Capacity = data.Capacity;
            }

            if (data.EducationId > 0)
            {
                group.EducationId = data.EducationId;
            }

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

            return datas.Select(m => new GroupDTo
            {
                Id = m.Id,
                Name = m.Name,
                Capacity = m.Capacity,
                EducationId = m.EducationId
            }).ToList();
        }

        public async Task<List<GroupDTo>> GetAllWithEducationIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(id);

            var datas = await _groupRepository.GetAllWithEducationIdAsync(id);

            return datas.Select(m => new GroupDTo
            {
                Name = m.Name,
                Capacity = m.Capacity,
                EducationId = m.EducationId
            }).ToList();
        }

        public async Task<List<GroupDTo>> FilterByEducationNameAsync(string name)
        {
            ArgumentNullException.ThrowIfNull(name);

            var datas = await _groupRepository.FilterByEducationNameAsync(name);

            return datas.Select(m => new GroupDTo
            {
                Name = m.Name,
                Capacity = m.Capacity,
                EducationId = m.EducationId
            }).ToList();
        }

        public async Task<List<GroupDTo>> SortWithCapacityAsync(string sortCondition)
        {
            ArgumentNullException.ThrowIfNull(sortCondition);

            var datas = await _groupRepository.SortWithCapacityAsync(sortCondition);

            if (datas is null)
            {
                throw new FormatException(ResponseMessages.InvalidSortingFormat);
            }

            return datas
                .Select(m => new GroupDTo
                {
                    Name = m.Name,
                    Capacity = m.Capacity,
                    EducationId = m.EducationId
                })
                .ToList();
        }

        public async Task<List<GroupDTo>> SearchByNameAsync(string searchText)
        {
            ArgumentNullException.ThrowIfNull(searchText);

            var foundDatas = await _groupRepository.SearchByNameAsync(searchText);

            if (foundDatas.Count == 0)
            {
                throw new NotFoundException(ResponseMessages.DataNotFound);
            }

            return foundDatas
                .Select(m => new GroupDTo
                {
                    Name = m.Name,
                    Capacity = m.Capacity,
                    EducationId = m.EducationId
                })
                .ToList();
        }

        public async Task<GroupDTo> GetByIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(id);

            var data = await _groupRepository.GetByIdAsync(id) ?? throw new NotFoundException(ResponseMessages.DataNotFound);

            return new GroupDTo
            {
                Name = data.Name,
                Capacity = data.Capacity,
                EducationId = data.EducationId
            };
        }
    }
}
