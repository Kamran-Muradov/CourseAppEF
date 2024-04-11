using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Models;
using Service.Helpers.Constants;
using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;

namespace CourseAppEF.Controllers
{
    public class AccountController
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        public static bool IsLoggedIn { get; private set; }

        public AccountController()
        {
            _accountService = new AccountService();
            _userService = new UserService();
        }

        public async Task RegisterAsync()
        {
            try
            {
                var allUsers = await _userService.GetAllAsync();

                ConsoleColor.Yellow.WriteConsole("Enter full name:");
            FullName: string fullName = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(fullName))
                {
                    ConsoleColor.Red.WriteConsole("Full name is required");
                    goto FullName;
                }

                if (!Regex.IsMatch(fullName, @"^[\p{L}]+(?:\s[\p{L}]+)?$"))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.InvalidFullNameFormat);
                    goto FullName;
                }

                ConsoleColor.Yellow.WriteConsole("Enter username:");
            UserName: string userName = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(userName))
                {
                    ConsoleColor.Red.WriteConsole("Username is required");
                    goto UserName;
                }

                if (allUsers.Any(m => m.UserName.ToLower() == userName.ToLower()))
                {
                    ConsoleColor.Red.WriteConsole("Username is already in use");
                    goto UserName;
                }

                ConsoleColor.Yellow.WriteConsole("Enter email:");
            Email: string email = Console.ReadLine().Trim().ToLower();

                if (string.IsNullOrEmpty(email))
                {
                    ConsoleColor.Red.WriteConsole("Email is required");
                    goto Email;
                }

                if (!Regex.IsMatch(email, @"^[a-zA-Z0-9!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$"))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.InvalidEmailFormat);
                    goto Email;
                }

                if (allUsers.Any(m => m.Email == email))
                {
                    ConsoleColor.Red.WriteConsole("Email is already in use");
                    goto Email;
                }

                ConsoleColor.Yellow.WriteConsole("Enter password:");
            Password: string password = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(password))
                {
                    ConsoleColor.Red.WriteConsole("Password is required");
                    goto Password;
                }

                if (!Regex.IsMatch(password, "[0-9]+"))
                {
                    ConsoleColor.Red.WriteConsole("Password must contain at least one number");
                    goto Password;
                }

                if (!Regex.IsMatch(password, @"\p{Lu}\p{Ll}?\d*"))
                {
                    ConsoleColor.Red.WriteConsole("Password must contain at least one uppercase letter");
                    goto Password;
                }

                if (!Regex.IsMatch(password, ".{8,}"))
                {
                    ConsoleColor.Red.WriteConsole("Password must contain at least 8 characters");
                    goto Password;
                }

                await _accountService.RegisterAsync(new User
                {
                    FullName = fullName,
                    UserName = userName,
                    Email = email,
                    Password = password,
                    CreatedDate = DateTime.Now
                });

                ConsoleColor.Green.WriteConsole("Register is successful");
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }

        public async Task LoginAsync()
        {
            ConsoleColor.Yellow.WriteConsole("Enter username or email (Press Enter to cancel):");
        UserNameOrEmail: string userNameOrEmail = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(userNameOrEmail))
            {
               return;
            }

            ConsoleColor.Yellow.WriteConsole("Enter password:");
        Password: string password = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(password))
            {
                ConsoleColor.Red.WriteConsole("Password is required");
                goto Password;
            }

            try
            {
                var checkLogin = await _accountService.LoginAsync(userNameOrEmail, password);

                if (checkLogin)
                {
                    IsLoggedIn = true;
                    ConsoleColor.Green.WriteConsole("Login success");
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Login failed");
                    goto UserNameOrEmail;
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }
    }
}
