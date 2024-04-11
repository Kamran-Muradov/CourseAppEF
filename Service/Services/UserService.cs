using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.Users;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public async Task<List<UserDTo>> GetAllAsync()
        {
            var datas = await _userRepository.GetAllAsync();

            return datas.Select(m => new UserDTo
            {
                FullName = m.FullName,
                UserName = m.UserName,
                Email = m.Email,
                Password = m.Password
            }).ToList();
        }
    }
}
