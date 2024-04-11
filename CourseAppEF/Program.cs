
using System;
using CourseAppEF.Controllers;
using Service.Helpers.Enums;
using Service.Helpers.Extensions;
using Spectre.Console;

AnsiConsole.Write(
    new FigletText("WELCOME")
        .Centered()
        .Color(Color.DarkOliveGreen1));

Top: AccountController accountController = new();

while (!AccountController.IsLoggedIn)
{
Operation: ConsoleColor.Cyan.WriteConsole("\nSelect one operation:\n\n" +
                                "+--------------------------------------" + "------------------+\n" +
                                "| 1. Login                 |" + "  2. Register                |\n" +
                                "+--------------------------------------------------------+");

    string operationStr = Console.ReadLine();

    int operation;

    bool isCorrectOperationFormat = int.TryParse(operationStr, out operation);

    if (isCorrectOperationFormat)
    {
        switch (operation)
        {
            case 1:
                await accountController.LoginAsync();
                break;

            case 2:
                await accountController.RegisterAsync();
                goto Operation;

            default:
                ConsoleColor.Red.WriteConsole("Operation is wrong, please try again");
                goto Operation;
        }
    }
    else
    {
        ConsoleColor.Red.WriteConsole("Operation format is wrong, try again:");
        goto Operation;
    }
}

EducationController educationController = new();

GroupController groupController = new();

while (true)
{
Operation: ShowMenu();
    string operationStr = Console.ReadLine();

    int operation;

    bool isCorrectOperationFormat = int.TryParse(operationStr, out operation);

    if (isCorrectOperationFormat)
    {
        switch (operation)
        {
            case (int)OperationType.CreateEducation:
                await educationController.CreateAsync();
                break;

            case (int)OperationType.UpdateEducation:
                await educationController.UpdateAsync();
                break;

            case (int)OperationType.DeleteEducation:
                await educationController.DeleteAsync();
                break;

            case (int)OperationType.GetAllEducations:
                await educationController.GetAllAsync();
                break;

            case (int)OperationType.GetAllEducationsWithGroups:
                await educationController.GetAllWithGroupsAsync();
                break;

            case (int)OperationType.GetEducationById:
                await educationController.GetByIdAsync();
                break;

            case (int)OperationType.SortEducationsWithCreatedDate:
                await educationController.SortWithCreateDateAsync();
                break;

            case (int)OperationType.SearchEducationsByName:
                await educationController.SearchByNameAsync();
                break;

            case (int)OperationType.CreateGroup:
                await groupController.CreateAsync();
                break;

            case (int)OperationType.UpdateGroup:
                await groupController.UpdateAsync();
                break;

            case (int)OperationType.DeleteGroup:
                await groupController.DeleteAsync();
                break;

            case (int)OperationType.GetAllGroups:
                await groupController.GetAllAsync();
                break;

            case (int)OperationType.GetAllGroupsWithEducationId:
                await groupController.GetAllWithEducationId();
                break;

            case (int)OperationType.GetGroupById:
                await groupController.GetByIdAsync();
                break;

            case (int)OperationType.FilterGroupsByEducationName:
                await groupController.FilterByEducationName();
                break;

            case (int)OperationType.SortGroupsWithCapacity:
                await groupController.SortWithCapacity();
                break;

            case (int)OperationType.SearchGroupsByName:
                await groupController.SearchByNameAsync();
                break;

            case (int)OperationType.Logout:
                Console.WriteLine("Are you sure? (Press 'Y' for yes, 'N' for no)");
            ExitChoice: string exitChoice = Console.ReadLine().Trim().ToLower();

                switch (exitChoice)
                {
                    case "n":
                        goto Operation;
                    case "y":
                        AccountController.IsLoggedIn = false;
                        goto Top;
                    default:
                        ConsoleColor.Red.WriteConsole("Wrong operation. Please try again:");
                        goto ExitChoice;
                }

            default:
                ConsoleColor.Red.WriteConsole("Operation is wrong, please try again");
                goto Operation;
        }
    }
    else
    {
        ConsoleColor.Red.WriteConsole("Operation format is wrong, try again:");
        goto Operation;
    }
}

static void ShowMenu()
{
    ConsoleColor.Cyan.WriteConsole("\nSelect one operation:\n\n" +
                                   "+--------------------------------------" + "-----------------------------------------+\n" +
                                   "| Education operations                |" + " Group operations                        |\n" +
                                   "|-------------------------------------|" + "-----------------------------------------|\n" +
                                   "| 1. Create education                 |" + "  9. Create group                        |\n" +
                                   "| 2. Update education                 |" + " 10. Update group                        |\n" +
                                   "| 3. Delete education                 |" + " 11. Delete group                        |\n" +
                                   "| 4. Show all educations              |" + " 12. Show all groups                     |\n" +
                                   "| 5. Show all educations with groups  |" + " 13. Show all groups by education id     |\n" +
                                   "| 6. Show education by id             |" + " 14. Show group by id                    |\n" +
                                   "| 7. Sort educations by create date   |" + " 15. Filter groups by education name     |\n" +
                                   "| 8. Search educations by name        |" + " 16. Sort groups by capacity             |\n" +
                                   "| 0. Log out                          |" + " 17. Search groups by name               |\n" +
                                   "+-------------------------------------------------------------------------------+");
}
