
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories;
using Service.Services;

Console.WriteLine();

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






foreach (var education in educations)
{
    Console.WriteLine(education.Education+"-"+string.Join(", ",education.Groups));
}

//await repository.UpdateAsync(new Education
//{
//    Id = 3,
//    Name = "Kamran"
//});


//var byIdAsync = await repository.GetByIdAsync(3);

//Console.WriteLine(byIdAsync.Name);



//Console.WriteLine(Test(email));
