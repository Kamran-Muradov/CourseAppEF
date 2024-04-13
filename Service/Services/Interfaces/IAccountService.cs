using Service.DTOs.Users;

namespace Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task RegisterAsync(UserCreateDTo data);
        Task<bool> LoginAsync(string usernameOrEmail, string password);
    }
}
