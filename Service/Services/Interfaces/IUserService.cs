using Service.DTOs.Users;

namespace Service.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTo>> GetAllAsync();
    }
}
