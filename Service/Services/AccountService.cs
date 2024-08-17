using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.Users;
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

        public async Task RegisterAsync(UserCreateDTo data)
        {
            ArgumentNullException.ThrowIfNull(data);

            await _userRepository.CreateAsync(new User
            {
                FullName = data.FullName,
                UserName = data.UserName,
                Email = data.Email,
                Password = data.Password,
                CreatedDate = DateTime.Now
            });
        }

        public async Task<bool> LoginAsync(string usernameOrEmail, string password)
        {
            var datas = await _userRepository.GetAllAsync();

            return datas.Any(m => (string.Equals(m.UserName, usernameOrEmail, StringComparison.CurrentCultureIgnoreCase) || string.Equals(m.Email, usernameOrEmail, StringComparison.CurrentCultureIgnoreCase))
                                  && m.Password == password);
        }
    }
}
