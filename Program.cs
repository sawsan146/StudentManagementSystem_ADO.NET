using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using StudentManagementSystem_ADO.NET.Entities;
using StudentManagementSystem_ADO.NET.Repositories;

namespace StudentManagementSystem_ADO.NET
{
    internal class Program
    {
        static void Main(string[] args)
        {

           IConfiguration configration = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json").Build();


            IRepository<Students> studentRepo = new StudentRepository<Students>(configration);

            // Students student = new Students{ FirstName="Mona",LastName="Yasser", Age=21, Grade=80m, Email="Mona.gemail.com" };

            //studentRepo.Add(student);

            //   studentRepo.DeleteById(18);


            //IEnumerable<Students> AllStudents = studentRepo.GetAll();
            //foreach (var s in AllStudents)
            //{
            //    Console.WriteLine(s);   
            //}

            Students st= studentRepo.GetByID(9);
           // Console.WriteLine(st);
          
           // st.Age = 22;
            studentRepo.Update(st);
            Console.WriteLine(st);

        }
    }
}
