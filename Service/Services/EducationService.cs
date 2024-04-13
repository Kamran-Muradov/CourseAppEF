using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.Educations;
using Service.Helpers.Constants;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _educationRepository;

        public EducationService()
        {
            _educationRepository = new EducationRepository();
        }

        public async Task CreateAsync(EducationCreateDTo data)
        {
            ArgumentNullException.ThrowIfNull(data);

            await _educationRepository.CreateAsync(new Education
            {
                Name = data.Name,
                Color = data.Color,
                CreatedDate = DateTime.Now
            });
        }

        public async Task UpdateAsync(EducationUpdateDTo data)
        {
            ArgumentNullException.ThrowIfNull(data);

            Education education = await _educationRepository.GetByIdAsync(data.Id) ?? throw new NotFoundException(ResponseMessages.DataNotFound);

            if (!string.IsNullOrEmpty(data.Name))
            {
                education.Name = data.Name;
            }

            if (!string.IsNullOrEmpty(data.Color))
            {
                education.Color = data.Color;
            }

            await _educationRepository.UpdateAsync(education);
        }

        public async Task DeleteAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(id);

            Education education = await _educationRepository.GetByIdAsync(id) ?? throw new NotFoundException(ResponseMessages.DataNotFound);

            await _educationRepository.DeleteAsync(education);
        }

        public async Task<List<EducationDTo>> GetAllAsync()
        {
            var datas = await _educationRepository.GetAllAsync();

            return datas
                .Select(m => new EducationDTo
                {
                    Id = m.Id,
                    Name = m.Name,
                    Color = m.Color,
                    CreatedDate = m.CreatedDate
                })
                .ToList();
        }

        public async Task<List<EducationWithGroupsDTo>> GetAllWithGroupsAsync()
        {
            var datas = await _educationRepository.GetAllWithGroupsAsync();

            return datas
                .Select(m => new EducationWithGroupsDTo
                {
                    Education = m.Name,
                    Groups = m.Groups.Select(m => m.Name).ToList()
                })
                .ToList();
        }

        public async Task<List<EducationDTo>> SortWithCreatedDateAsync(string sortCondition)
        {
            ArgumentNullException.ThrowIfNull(sortCondition);

            var datas = await _educationRepository.SortWithCreatedDateAsync(sortCondition);

            if (datas is null)
            {
                throw new FormatException(ResponseMessages.InvalidSortingFormat);
            }

            return datas
                .Select(m => new EducationDTo
                {
                    Name = m.Name,
                    Color = m.Color,
                    CreatedDate = m.CreatedDate
                })
                .ToList();
        }

        public async Task<List<EducationDTo>> SearchByNameAsync(string searchText)
        {
            ArgumentNullException.ThrowIfNull(searchText);

            var foundDatas = await _educationRepository.SearchByNameAsync(searchText);

            if (foundDatas.Count == 0)
            {
                throw new NotFoundException(ResponseMessages.DataNotFound);
            }

            return foundDatas
            .Select(m => new EducationDTo
            {
                Name = m.Name,
                Color = m.Color,
                CreatedDate = m.CreatedDate
            })
            .ToList();
        }

        public async Task<EducationDTo> GetByIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(id);

            var data = await _educationRepository.GetByIdAsync(id) ?? throw new NotFoundException(ResponseMessages.DataNotFound);

            return new EducationDTo { Name = data.Name, Color = data.Color, CreatedDate = data.CreatedDate };
        }
    }
}
