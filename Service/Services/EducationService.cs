using Domain.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task CreateAsync(Education education)
        {
            ArgumentNullException.ThrowIfNull(education);

            await _educationRepository.CreateAsync(education);
        }

        public async Task UpdateAsync(Education education)
        {
            ArgumentNullException.ThrowIfNull(education);

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
            return await _educationRepository.GetAll()
                .Select(m => new EducationDTo
                {
                    Name = m.Name,
                    Color = m.Color
                }).ToListAsync();
        }

        public async Task<List<EducationWithGroupsDTo>> GetAllWithGroupsAsync()
        {
            return await _educationRepository.GetAll()
                .Include(m => m.Groups)
                .Select(m => new EducationWithGroupsDTo
                {
                    Education = m.Name,
                    Groups = m.Groups.Select(m => m.Name).ToList()
                }).ToListAsync();
        }

        public async Task<List<EducationDTo>> SortWithCreatedDate(string sortCondition)
        {
            var datas = await _educationRepository.GetAll()
                .Select(m => new EducationDTo
                {
                    Name = m.Name,
                    Color = m.Color,
                    CreatedDate = m.CreatedDate
                }).ToListAsync();

            switch (sortCondition)
            {
                case "asc":
                    return datas.OrderBy(m => m.CreatedDate).ToList();
                case "desc":
                    return datas.OrderByDescending(m => m.CreatedDate).ToList();
                default:
                    throw new FormatException(ResponseMessages.InvalidSortingFormat);
            }
        }

        public async Task<EducationDTo> GetByIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(id);

            var data = await _educationRepository.GetByIdAsync(id) ?? throw new NotFoundException(ResponseMessages.DataNotFound);

            return new EducationDTo
            {
                Name = data.Name,
                Color = data.Color,
            };
        }
    }
}
