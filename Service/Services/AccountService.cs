using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService()
        {
            _userRepository = new UserRepository();
        }

        public async Task RegisterAsync(User data)
        {
            ArgumentNullException.ThrowIfNull(data);

            await _userRepository.CreateAsync(data);
        }

        public async Task<bool> LoginAsync(string usernameOrEmail, string password)
        {
            var datas = await _userRepository.GetAllAsync();

            return datas.Any(m => (m.UserName.ToLower() == usernameOrEmail.ToLower() || m.Email.ToLower() == usernameOrEmail.ToLower())
                                  && m.Password == password);
        }
    }
}
