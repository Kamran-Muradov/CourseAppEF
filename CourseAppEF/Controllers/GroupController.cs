using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleTables;
using Domain.Models;
using Service.DTOs.Groups;
using Service.Helpers.Constants;
using Service.Helpers.Extensions;
using Service.Services;
using Service.Services.Interfaces;

namespace CourseAppEF.Controllers
{
    public class GroupController
    {
        private readonly IGroupService _groupService;
        private readonly IEducationService _educationService;

        public GroupController()
        {
            _groupService = new GroupService();
            _educationService = new EducationService();
        }

        public async Task CreateAsync()
        {
            try
            {
                var allEducations = await _educationService.GetAllAsync();
                var allGroups = await _groupService.GetAllAsync();

                if (allEducations.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole("There is not any education. Please create one");
                    return;
                }

                ConsoleColor.Yellow.WriteConsole("Enter name: (Press Enter to cancel)");
            Name:
                string name = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(name))
                {
                    return;
                }

                if (allGroups.Any(m => m.Name.ToLower() == name.ToLower()))
                {
                    ConsoleColor.Red.WriteConsole("Group with this name already exists");
                    goto Name;
                }

                ConsoleColor.Yellow.WriteConsole("Enter capacity:");
            Capacity: string capacityStr = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(capacityStr))
                {
                    ConsoleColor.Red.WriteConsole("Capacity is required");
                    goto Capacity;
                }

                int capacity;

                if (!int.TryParse(capacityStr, out capacity))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.InvalidCapacityFormat + ". Please try again:");
                    goto Capacity;
                }

                if (capacity < 10)
                {
                    ConsoleColor.Red.WriteConsole("Capacity must be minimum 10. Please try again:");
                    goto Capacity;
                }

                Console.WriteLine();
                ConsoleColor.Yellow.WriteConsole("Educations:");
                allEducations.PrintAllWithId();

                ConsoleColor.Yellow.WriteConsole("Enter id of the education you want to add group:");
            EducationId: string educationIdStr = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(educationIdStr))
                {
                    ConsoleColor.Red.WriteConsole("Education id is required");
                    goto EducationId;
                }

                int educationId;

                if (!int.TryParse(educationIdStr, out educationId))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.InvalidIdFormat + ". Please try again:");
                    goto EducationId;
                }

                if (educationId < 1)
                {
                    ConsoleColor.Red.WriteConsole("Id cannot be less than 1. Please try again:");
                    goto EducationId;
                }

                if (allEducations.All(m => m.Id != educationId))
                {
                    ConsoleColor.Red.WriteConsole("There is no education with specified id. Please try again:");
                    goto EducationId;
                }

                await _groupService.CreateAsync(new Group
                {
                    Name = name,
                    Capacity = capacity,
                    EducationId = educationId,
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
            var allEducations = await _educationService.GetAllAsync();
            var allGroups = await _groupService.GetAllAsync();

            if (allGroups.Count == 0)
            {
                ConsoleColor.Red.WriteConsole("There is not any group. Please create one");
                return;
            }

            ConsoleColor.Yellow.WriteConsole("Groups");
            allGroups.PrintAllWithId();

            ConsoleColor.Yellow.WriteConsole("Enter id of the group you want to update: (Press Enter to cancel)");
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

            if (allGroups.All(m => m.Id != id))
            {
                ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                return;
            }

            ConsoleColor.Yellow.WriteConsole("Enter name (Press Enter if you don't want to change):");
        UpdatedName: string updatedName = Console.ReadLine().Trim();

            if (allGroups.Any(m => m.Name.ToLower() == updatedName.ToLower()))
            {
                ConsoleColor.Red.WriteConsole("Group with this name already exists");
                goto UpdatedName;
            }

            ConsoleColor.Yellow.WriteConsole("Enter capacity (Press Enter if you don't want to change):");
        Age: string updatedCapacityStr = Console.ReadLine();

            int updatedCapacity = 0;

            if (!string.IsNullOrWhiteSpace(updatedCapacityStr))
            {
                if (!int.TryParse(updatedCapacityStr, out updatedCapacity))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.InvalidCapacityFormat + ". Please try again:");
                    goto Age;
                }

                if (updatedCapacity < 10)
                {
                    ConsoleColor.Red.WriteConsole("Capacity must be minimum 10. Please try again:");
                    goto Age;
                }
            }

            Console.WriteLine();
            ConsoleColor.Yellow.WriteConsole("Educations:");
            allEducations.PrintAllWithId();

            ConsoleColor.Yellow.WriteConsole("Enter education id you want to switch (Press Enter if you don't want to change):");
        EducationId: string educationIdStr = Console.ReadLine();

            int updatedEducationId = 0;

            if (!string.IsNullOrWhiteSpace(educationIdStr))
            {
                if (!int.TryParse(educationIdStr, out updatedEducationId))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.InvalidIdFormat + ". Please try again:");
                    goto EducationId;
                }

                if (updatedEducationId < 1)
                {
                    ConsoleColor.Red.WriteConsole("Id cannot be less than 1. Please try again:");
                    goto EducationId;
                }

                if (allEducations.All(m => m.Id != updatedEducationId))
                {
                    ConsoleColor.Red.WriteConsole("There is no education with specified id. Please try again:");
                    goto EducationId;
                }
            }

            await _groupService.UpdateAsync(new Group
            {
                Id = id,
                Name = updatedName,
                Capacity = updatedCapacity,
                EducationId = updatedEducationId
            });

            ConsoleColor.Green.WriteConsole(ResponseMessages.UpdateSuccess);
        }

        public async Task DeleteAsync()
        {
            try
            {
                var allGroups = await _groupService.GetAllAsync();

                if (allGroups.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole("There is not any group. Please create one");
                    return;
                }

                ConsoleColor.Yellow.WriteConsole("Groups:");

                allGroups.PrintAllWithId();

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

                if (allGroups.All(m => m.Id != id))
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
                        await _groupService.DeleteAsync(id);

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
                var response = await _groupService.GetAllAsync();

                if (response.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole("There is not any group. Please create one");
                    return;
                }

                var table = new ConsoleTable("Name", "Capacity", "EducationId");

                foreach (var item in response)
                {
                    table.AddRow(item.Name, item.Capacity, item.EducationId);
                }

                table.Options.EnableCount = false;

                table.Write();
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }

        public async Task GetAllWithEducationId()
        {
            try
            {
                var allGroups = await _groupService.GetAllAsync();

                if (allGroups.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole("There is not any group. Please create one");
                    return;
                }

                ConsoleColor.Yellow.WriteConsole("Enter education id of the group: (Press Enter to cancel)");
            EducationId: string idStr = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(idStr))
                {
                    return;
                }

                int id;

                if (!int.TryParse(idStr, out id))
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.InvalidIdFormat + ". Please try again:");
                    goto EducationId;
                }

                if (id < 1)
                {
                    ConsoleColor.Red.WriteConsole("Education id cannot be less than 1. Please try again:");
                    goto EducationId;
                }

                var response = await _groupService.GetAllWithEducationIdAsync(id);

                if (response.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                    return;
                }

                var table = new ConsoleTable("Name", "Capacity", "EducationId");

                foreach (var item in response)
                {
                    table.AddRow(item.Name, item.Capacity, item.EducationId);
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
                var allGroups = await _groupService.GetAllAsync();

                if (allGroups.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole("There is not any group. Please create one");
                    return;
                }

                ConsoleColor.Yellow.WriteConsole("Enter id of the group: (Press Enter to cancel)");
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

                var response = await _groupService.GetByIdAsync(id);

                var table = new ConsoleTable("Name", "Capacity", "EducationId");

                table.AddRow(response.Name, response.Capacity, response.EducationId);

                table.Options.EnableCount = false;

                table.Write();

            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }

        public async Task FilterByEducationName()
        {
            try
            {
                var allGroups = await _groupService.GetAllAsync();

                if (allGroups.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole("There is not any group. Please create one");
                    return;
                }

                ConsoleColor.Yellow.WriteConsole("Enter education name (Press Enter to cancel):");
                string educationName = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(educationName))
                {
                    return;
                }

                var response = await _groupService.FilterByEducationNameAsync(educationName);

                if (response.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                    return;
                }

                var table = new ConsoleTable("Name", "Capacity", "EducationId");

                foreach (var item in response)
                {
                    table.AddRow(item.Name, item.Capacity, item.EducationId);
                }

                table.Options.EnableCount = false;

                table.Write();
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }

        public async Task SortWithCapacity()
        {
            try
            {
                var allGroups = await _groupService.GetAllAsync();

                if (allGroups.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole("There is not any group. Please create one");
                    return;
                }

                ConsoleColor.Yellow.WriteConsole("Enter sorting condition ('asc' or 'desc')");
            SortCondition: string sortCondition = Console.ReadLine().Trim().ToLower();

                if (string.IsNullOrEmpty(sortCondition))
                {
                    return;
                }

                List<GroupDTo> response;

                switch (sortCondition)
                {
                    case "asc":
                        response = await _groupService.SortWithCapacityAsync(sortCondition);
                        break;
                    case "desc":
                        response = await _groupService.SortWithCapacityAsync(sortCondition);
                        break;
                    default:
                        ConsoleColor.Red.WriteConsole(ResponseMessages.InvalidSortingFormat + ". Please try again:");
                        goto SortCondition;
                }

                var table = new ConsoleTable("Name", "Capacity", "EducationId");

                foreach (var item in response)
                {
                    table.AddRow(item.Name, item.Capacity, item.EducationId);
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
                var allGroups = await _groupService.GetAllAsync();

                if (allGroups.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole("There is not any group. Please create one");
                    return;
                }

                ConsoleColor.Yellow.WriteConsole("Enter search text: (Press Enter to cancel)");

                string searchText = Console.ReadLine().Trim().ToLower();

                if (string.IsNullOrEmpty(searchText))
                {
                    return;
                }

                var response = await _groupService.SearchByNameAsync(searchText);

                if (response.Count == 0)
                {
                    ConsoleColor.Red.WriteConsole(ResponseMessages.DataNotFound);
                    return;
                }

                var table = new ConsoleTable("Name", "Capacity", "EducationId");

                foreach (var item in response)
                {
                    table.AddRow(item.Name, item.Capacity, item.EducationId);
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
