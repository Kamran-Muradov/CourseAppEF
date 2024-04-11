using Domain.Models;

namespace Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task RegisterAsync(User data);
        Task<bool> LoginAsync(string usernameOrEmail, string password);
    }
}
