using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using StudentManagementSystem_ADO.NET.Entities;
using StudentManagementSystem_ADO.NET.Repositories;

namespace StudentManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            IRepository<Students> studentRepo = new StudentRepository<Students>(configuration);

            while (true)
            {
                // Console.Clear(); 
                Console.WriteLine("\n=============================================\n");
                Console.WriteLine("Student Management System");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View Students");
                Console.WriteLine("3. Update Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                int choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n=============================================\n");

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("Enter First Name: ");
                        string fName = Console.ReadLine();  
                        Console.Write("Enter Last Name: ");
                        string lNAme = Console.ReadLine();
                        Console.Write("Enter Age: ");
                        int age = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Enter Grade: ");
                        decimal Grade = decimal.Parse(Console.ReadLine());

                        Students newStudent = new Students { FirstName = fName, LastName = lNAme, Age = age, Email = email, Grade = Grade };
                        studentRepo.Add(newStudent);
                        Console.WriteLine("Student added successfully.");
                        break;

                    case 2:
                        Console.Clear();
                        var students = studentRepo.GetAll();
                        foreach (var student in students)
                        {
                            Console.WriteLine(student);
                        }
                        break;

                    case 3:
                        Console.Clear();
                        Console.Write("Enter Student ID to update: ");
                        int idToUpdate = Convert.ToInt32(Console.ReadLine());
                        Students studentToUpdate = studentRepo.GetByID(idToUpdate);
                        if (studentToUpdate != null)
                        {
                            Console.Write("Enter new Age: ");
                            studentToUpdate.Age = Convert.ToInt32(Console.ReadLine());
                            studentRepo.Update(studentToUpdate);
                            Console.WriteLine("Student updated successfully.");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Student not found.");
                        }
                        break;

                    case 4:
                        Console.Clear();
                        Console.Write("Enter Student ID to delete: ");
                        int idToDelete = Convert.ToInt32(Console.ReadLine());
                        studentRepo.DeleteById(idToDelete);
                        Console.WriteLine("Student deleted successfully.");
                        break;

                    case 5:
                        Console.Clear();
                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
