
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories;

Console.WriteLine();

string email = "kamran@code.edu.az";


bool Test(string email)
{
    return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
}

EducationRepository repository= new EducationRepository();

//await repository.CreateAsync(new Education
//{
//    Name = "pb",
//    Color = "fdsfd",
//    CreatedDate = DateTime.Now
//});

var educations =  repository.GetAll().Include(m=>m.Groups).Select(m=>new Education
{
    Name = m.Name,
    Color = m.Color,
    Groups = m.Groups
});





foreach (var education in educations)
{
    Console.WriteLine(education.Name+"-"+string.Join(", ",education.Groups));
}

//await repository.UpdateAsync(new Education
//{
//    Id = 3,
//    Name = "Kamran"
//});


//var byIdAsync = await repository.GetByIdAsync(3);

//Console.WriteLine(byIdAsync.Name);



//Console.WriteLine(Test(email));
