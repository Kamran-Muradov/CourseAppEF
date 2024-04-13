using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Users
{
    public class UserCreateDTo
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
