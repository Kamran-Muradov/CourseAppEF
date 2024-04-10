using Domain.Models;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class EducationRepository : BaseRepository<Education>, IEducationRepository
    {
        public Task<List<Education>> SearchByName(string searchText)
        {
            throw new NotImplementedException();
        }
    }
}
