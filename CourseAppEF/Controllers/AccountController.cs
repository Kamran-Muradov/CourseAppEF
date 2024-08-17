using Service.DTOs.Users;
using Service.Helpers.Constants;
using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CourseAppEF.Controllers
{
    public class AccountController
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        public bool IsLoggedIn { get; set; }

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

                ConsoleColor.Yellow.WriteConsole("Enter full name (Press Enter to cancel):");
            FullName:
                string fullName = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(fullName))
                {
                    return;
                }

                if (!fullName.Contains(' ') || !Regex.IsMatch(fullName, @"^[\p{L}]+(?:\s[\p{L}]+)?$"))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.InvalidFullNameFormat);
                    goto FullName;
                }

                ConsoleColor.Yellow.WriteConsole("Enter username:");
            UserName:
                string userName = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(userName))
                {
                    ConsoleColor.Red.WriteConsole("Username is required");
                    goto UserName;
                }

                if (userName.Contains(' '))
                {
                    ConsoleColor.Red.WriteConsole("Username cannot contain white space");
                    goto UserName;
                }

                if (allUsers.Any(m => m.UserName.ToLower() == userName.ToLower()))
                {
                    ConsoleColor.Red.WriteConsole("Username is already in use");
                    goto UserName;
                }

                ConsoleColor.Yellow.WriteConsole("Enter email:");
            Email:
                string email = Console.ReadLine().Trim().ToLower();

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
            Password: string password = Console.ReadLine();

                if (string.IsNullOrEmpty(password))
                {
                    ConsoleColor.Red.WriteConsole("Password is required");
                    goto Password;
                }

                if (password.Contains(' '))
                {
                    ConsoleColor.Red.WriteConsole("Password cannot contain white space");
                    goto Password;
                }

                if (!Regex.IsMatch(password, "[0-9]+"))
                {
                    ConsoleColor.Red.WriteConsole("Password must contain at least one number");
                    goto Password;
                }

                if (!Regex.IsMatch(password, "[a-z]") || !Regex.IsMatch(password, "[A-Z]"))
                {
                    ConsoleColor.Red.WriteConsole("Password must contain at least one uppercase and one lowercase letter");
                    goto Password;
                }

                if (!Regex.IsMatch(password, ".{8,}"))
                {
                    ConsoleColor.Red.WriteConsole("Password must contain at least 8 characters");
                    goto Password;
                }

                ConsoleColor.Yellow.WriteConsole("Confirm password:");
            ConfirmPassword: string confirmPassword = Console.ReadLine();

                if (confirmPassword != password)
                {
                    ConsoleColor.Red.WriteConsole("Passwords do not match");
                    goto ConfirmPassword;
                }

                await _accountService.RegisterAsync(new UserCreateDTo
                {
                    FullName = fullName,
                    UserName = userName,
                    Email = email,
                    Password = password,
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
        UserNameOrEmail:
            string userNameOrEmail = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(userNameOrEmail))
            {
                return;
            }

            ConsoleColor.Yellow.WriteConsole("Enter password:");
        Password:
            string password = ReadPassword();

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

        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }

            Console.WriteLine();

            return password;
        }
    }
}
