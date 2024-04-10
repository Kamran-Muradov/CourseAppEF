
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories;
using Service.Helpers.Enums;
using Service.Helpers.Extensions;
using Service.Services;
using Spectre.Console;

AnsiConsole.Write(
    new FigletText("WELCOME")
        .Centered()
        .Color(Color.DarkOliveGreen1));

//while (true)
//{
//Operation: ShowMenu();
//    string operationStr = Console.ReadLine();

//    int operation;

//    bool isCorrectOperationFormat = int.TryParse(operationStr, out operation);

//    if (isCorrectOperationFormat)
//    {
//        switch (operation)
//        {
//            case (int)OperationType.CreateGroup:
//                groupController.Create();
//                break;

//            case (int)OperationType.UpdateGroup:
//                groupController.Update();
//                break;

//            case (int)OperationType.DeleteGroup:
//                groupController.Delete();
//                break;

//            case (int)OperationType.GetAllGroups:
//                groupController.GetAll();
//                break;

//            case (int)OperationType.GetAllGroupsByTeacher:
//                groupController.GetAllByTeacher();
//                break;

//            case (int)OperationType.GetAllGroupsByRoom:
//                groupController.GetAllByRoom();
//                break;

//            case (int)OperationType.GetGroupById:
//                groupController.GetById();
//                break;

//            case (int)OperationType.SearchGroupsByName:
//                groupController.SearchByName();
//                break;

//            case (int)OperationType.CreateStudent:
//                studentController.Create();
//                break;

//            case (int)OperationType.UpdateStudent:
//                studentController.Update();
//                break;

//            case (int)OperationType.DeleteStudent:
//                studentController.Delete();
//                break;

//            case (int)OperationType.GetAllStudents:
//                studentController.GetAll();
//                break;

//            case (int)OperationType.GetAllStudentsByAge:
//                studentController.GetAllByAge();
//                break;

//            case (int)OperationType.GetAllStudentsByGroupId:
//                studentController.GetAllByGroupId();
//                break;

//            case (int)OperationType.GetStudentById:
//                studentController.GetById();
//                break;

//            case (int)OperationType.SearchStudentsByNameOrSurname:
//                studentController.SearchByNameOrSurname();
//                break;

//            case 0:
//                Console.WriteLine("Are you sure? (Press 'Y' for yes, 'N' for no)");
//            ExitChoice: string exitChoice = Console.ReadLine().Trim().ToLower();

//                if (exitChoice == "n")
//                {
//                    goto Operation;
//                }
//                else if (exitChoice == "y")
//                {
//                    Environment.Exit(0);
//                }
//                else
//                {
//                    ConsoleColor.Red.WriteConsole("Wrong operation. Please try again:");
//                    goto ExitChoice;
//                }
//                break;

//            default:
//                ConsoleColor.Red.WriteConsole("Operation is wrong, please try again");
//                goto Operation;
//        }
//    }
//    else
//    {
//        ConsoleColor.Red.WriteConsole("Operation format is wrong, try again:");
//        goto Operation;
//    }
////}

static void ShowMenu()
{
    ConsoleColor.Cyan.WriteConsole("\nSelect one operation:\n\n" +
        "+-----------------------------------" + "-----------------------------------------+\n" +
        "| Education operations             |" + " Group operations                        |\n" +
        "|----------------------------------|" + "-----------------------------------------|\n" +
        "| 1. Create group                  |" + "  9. Create student                      |\n" +
        "| 2. Update group                  |" + " 10. Update student                      |\n" +
        "| 3. Delete group                  |" + " 11. Delete student                      |\n" +
        "| 4. Show all groups               |" + " 12. Show all students                   |\n" +
        "| 5. Show all groups by teacher    |" + " 13. Show all students by age            |\n" +
        "| 6. Show all groups by room       |" + " 14. Show all students by group id       |\n" +
        "| 7. Show group by id              |" + " 15. Show student by id                  |\n" +
        "| 8. Search groups by name         |" + " 16. Search students by name or surname  |\n" +
        "+----------------------------------------------------------------------------+\n\n" +
        "0. Exit");
}





string email = "kamran@code.edu.az";


bool Test(string email)
{
    return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
}

EducationService repository= new EducationService();

//await repository.CreateAsync(new Education
//{
//    Name = "pb",
//    Color = "fdsfd",
//    CreatedDate = DateTime.Now
//});

var educations = await repository.GetAllWithGroupsAsync();






//foreach (var education in educations)
//{
//    Console.WriteLine(education.Education+"-"+string.Join(", ",education.Groups));
//}

//await repository.UpdateAsync(new Education
//{
//    Id = 3,
//    Name = "Kamran"
//});


//var byIdAsync = await repository.GetByIdAsync(3);

//Console.WriteLine(byIdAsync.Name);



//Console.WriteLine(Test(email));
