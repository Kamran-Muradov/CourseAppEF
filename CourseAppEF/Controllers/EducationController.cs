using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleTables;
using Domain.Models;
using Service.DTOs.Educations;
using Service.Helpers.Constants;
using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;

namespace CourseAppEF.Controllers
{
    public class EducationController
    {
        private readonly IEducationService _educationService;

        public EducationController()
        {
            _educationService = new EducationService();
        }

        public async Task CreateAsync()
        {
            try
            {
                var allEducations = await _educationService.GetAllAsync();

                ConsoleColor.Yellow.WriteConsole("Enter name: (Press Enter to cancel)");
            Name:
                string name = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(name))
                {
                    return;
                }

                if (allEducations.Any(m => m.Name.ToLower() == name.ToLower()))
                {
                    ConsoleColor.Red.WriteConsole("Education with this name already exists");
                    goto Name;
                }

                ConsoleColor.Yellow.WriteConsole("Enter color:");
            Color:
                string color = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(color))
                {
                    ConsoleColor.Red.WriteConsole("Color is required");
                    goto Color;
                }

                await _educationService.CreateAsync(new Education
                {
                    Name = name,
                    Color = color,
                    CreatedDate = DateTime.Now
                });

                ConsoleColor.Green.WriteConsole(ResponseMessages.AddSuccess);
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }

        public async Task UpdateAsync()
        {
            try
            {
                var allEducations = await _educationService.GetAllAsync();

                if (allEducations.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole("There is not any education. Please create one");
                    return;
                }

                ConsoleColor.Yellow.WriteConsole("Educations:");

                allEducations.PrintAllWithId();

                ConsoleColor.Yellow.WriteConsole("Enter id of the education you want to update: (Press Enter to cancel)");
            Id:
                string idStr = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(idStr))
                {
                    return;
                }

                int id;

                if (!int.TryParse(idStr, out id))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.InvalidIdFormat + ". Please try again:");
                    goto Id;
                }

                if (id < 1)
                {
                    ConsoleColor.Red.WriteConsole("Id cannot be less than 1. Please try again:");
                    goto Id;
                }

                if (allEducations.All(m => m.Id != id))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                    return;
                }

                ConsoleColor.Yellow.WriteConsole("Enter name (Press Enter if you don't want to change):");
            UpdatedName: string updatedName = Console.ReadLine().Trim();

                if (allEducations.Any(m => m.Name.ToLower() == updatedName.ToLower()))
                {
                    ConsoleColor.Red.WriteConsole("Education with this name already exists");
                    goto UpdatedName;
                }

                ConsoleColor.Yellow.WriteConsole("Enter color (Press Enter if you don't want to change):");
                string updatedColor = Console.ReadLine().Trim();

                await _educationService.UpdateAsync(new Education
                { Id = id, Name = updatedName, Color = updatedColor });

                ConsoleColor.Green.WriteConsole(ResponseMessages.UpdateSuccess);
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }

        public async Task DeleteAsync()
        {
            try
            {
                var allEducations = await _educationService.GetAllAsync();

                if (allEducations.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole("There is not any education. Please create one");
                    return;
                }

                ConsoleColor.Yellow.WriteConsole("Educations:");

                allEducations.PrintAllWithId();

                ConsoleColor.Yellow.WriteConsole("Enter id of the education you want to delete: (Press Enter to cancel)");
            Id: string idStr = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(idStr))
                {
                    return;
                }

                int id;

                if (!int.TryParse(idStr, out id))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.InvalidIdFormat + ". Please try again:");
                    goto Id;
                }

                if (id < 1)
                {

                    ConsoleColor.Red.WriteConsole("Id cannot be less than 1. Please try again:");
                    goto Id;
                }

                if (allEducations.All(m => m.Id != id))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                    return;
                }

                Console.WriteLine("Are you sure you want to delete this student? (Press 'Y' for yes, 'N' for no)");
            DeleteChoice: string deleteChoice = Console.ReadLine().Trim().ToLower();

                switch (deleteChoice)
                {
                    case "n":
                        return;
                    case "y":
                        await _educationService.DeleteAsync(id);

                        ConsoleColor.Green.WriteConsole(ResponseMessages.DeleteSuccess);
                        break;
                    default:
                        ConsoleColor.Red.WriteConsole("Wrong operation. Please try again");
                        goto DeleteChoice;
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }

        public async Task GetAllAsync()
        {
            try
            {
                var response = await _educationService.GetAllAsync();

                if (response.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole("There is not any education. Please create one");
                    return;
                }

                var table = new ConsoleTable("Name", "Color");

                foreach (var item in response)
                {
                    table.AddRow(item.Name, item.Color);
                }

                table.Options.EnableCount = false;

                table.Write();
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }

        public async Task GetAllWithGroupsAsync()
        {
            try
            {
                var response = await _educationService.GetAllWithGroupsAsync();

                if (response.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole("There is not any education. Please create one");
                    return;
                }

                var table = new ConsoleTable("Name", "Groups");

                foreach (var item in response)
                {
                    table.AddRow(item.Education, string.Join(", ", item.Groups));
                }

                table.Options.EnableCount = false;

                table.Write();
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }

        public async Task GetByIdAsync()
        {
            try
            {
                var allEducations = await _educationService.GetAllAsync();

                if (allEducations.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole("There is not any education. Please create one");
                    return;
                }

                ConsoleColor.Yellow.WriteConsole("Enter id of the education: (Press Enter to cancel)");
            Id: string idStr = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(idStr))
                {
                    return;
                }

                int id;

                if (!int.TryParse(idStr, out id))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.InvalidIdFormat + ". Please try again:");
                    goto Id;
                }

                if (id < 1)
                {
                    ConsoleColor.Red.WriteConsole("Id cannot be less than 1. Please try again:");
                    goto Id;
                }

                var response = await _educationService.GetByIdAsync(id);

                var table = new ConsoleTable("Name", "Color");

                table.AddRow(response.Name, response.Color);

                table.Options.EnableCount = false;

                table.Write();

            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }

        public async Task SortWithCreateDateAsync()
        {
            try
            {
                var allEducations = await _educationService.GetAllAsync();

                if (allEducations.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole("There is not any education. Please create one");
                    return;
                }

                ConsoleColor.Yellow.WriteConsole("Enter sorting condition ('asc' or 'desc')");
            SortCondition: string sortCondition = Console.ReadLine().Trim().ToLower();

                if (string.IsNullOrEmpty(sortCondition))
                {
                    return;
                }

                List<EducationDTo> response;

                switch (sortCondition)
                {
                    case "asc":
                        response = await _educationService.SortWithCreatedDateAsync(sortCondition);
                        break;
                    case "desc":
                        response = await _educationService.SortWithCreatedDateAsync(sortCondition);
                        break;
                    default:
                        ConsoleColor.Red.WriteConsole(ResponseMessages.InvalidSortingFormat + ". Please try again:");
                        goto SortCondition;
                }

                var table = new ConsoleTable("Name", "Color", "Create date");

                foreach (var item in response)
                {
                    table.AddRow(item.Name, item.Color, item.CreatedDate.ToShortDateString());
                }

                table.Options.EnableCount = false;

                table.Write();

            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }

        public async Task SearchByNameAsync()
        {
            try
            {
                var allEducations = await _educationService.GetAllAsync();

                if (allEducations.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole("There is not any education. Please create one");
                    return;
                }

                ConsoleColor.Yellow.WriteConsole("Enter search text: (Press Enter to cancel)");

                string searchText = Console.ReadLine().Trim().ToLower();

                if (string.IsNullOrEmpty(searchText))
                {
                    return;
                }

                var response = await _educationService.SearchByNameAsync(searchText);

                if (response.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                    return;
                }

                var table = new ConsoleTable("Name", "Color");

                foreach (var item in response)
                {
                    table.AddRow(item.Name, item.Color);
                }

                table.Options.EnableCount = false;

                table.Write();
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }
    }
}
