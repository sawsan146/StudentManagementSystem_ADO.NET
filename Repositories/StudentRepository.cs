using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using StudentManagementSystem_ADO.NET.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem_ADO.NET.Repositories
{
    public class StudentRepository<T> : IRepository<T> where T : Students
    {


        private readonly string ConnectionString;

        public StudentRepository(IConfiguration _configration)
        {
            ConnectionString = _configration.GetConnectionString("DefaultConnection");
        }

        public void Add(T entity)
        {
            var sql = "insert into students (FirstName,LastName,Age,Grade,Email) values (@FirstName,@LastName,@Age,@Grade,@Email)";


            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(sql, conn))
            {

                command.Parameters.AddWithValue("@FirstName", entity.FirstName);
                command.Parameters.AddWithValue("@LastName", entity.LastName);
                command.Parameters.AddWithValue("@Age", entity.Age);
                command.Parameters.AddWithValue("@Grade", entity.Grade);
                command.Parameters.AddWithValue("@Email", entity.Email);

                conn.Open();
                command.ExecuteNonQuery();
            }

        }

        public void DeleteById(int ID)
        {
            string checkSql = "SELECT COUNT(*) FROM students WHERE StudentId=@ID";
            string sql = "Delete from students where StudentId=@ID";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand checkCommand = new SqlCommand(checkSql, conn))
                {
                    checkCommand.Parameters.AddWithValue("@ID", ID);
                    int count = (int)checkCommand.ExecuteScalar(); 

                    if (count == 0)
                    {
                        Console.WriteLine("No student found with the given ID.");
                        return; 
                    }
                }

                using (SqlCommand deleteCommand = new SqlCommand(sql, conn))
                {
                    deleteCommand.Parameters.AddWithValue("@ID", ID);
                    deleteCommand.ExecuteNonQuery();
                    Console.WriteLine("Student Deleted Successfully !");
                }
            }
        }

        public IEnumerable<T> GetAll()
        {

            var sql = "Select * From Students;";

            using(var conn=new SqlConnection(ConnectionString))
            using(var command=new SqlCommand(sql, conn))
            {
                conn.Open();
                SqlDataReader rd = command.ExecuteReader();
                Students student;

                while (rd.Read())
                {
                    student = new Students
                    {
                        ID = rd.GetInt32(0),
                        FirstName = rd.GetString(1),
                        LastName = rd.GetString(2),
                        Age = rd.GetInt32(3),
                        Grade = rd.GetDecimal(4),
                        Email = rd.GetString(5)

                    };
                    yield return (T)student;
                }
            }
           

        }

        public T GetByID(int Id)
        {
            string checkSql = "Select count(*) from students where studentID=@ID;";
            string sql = "Select * from students Where studentID=@ID;";

            using (var conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using (var checkCommand=new SqlCommand(checkSql,conn))
                {
                    checkCommand.Parameters.AddWithValue("@ID", Id);
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count == 0)
                    {
                        Console.WriteLine("No student found with the given ID.");
                        return default (T);
                    }
                }

                using(var command=new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@ID", Id);

                    SqlDataReader rd = command.ExecuteReader();

                  rd.Read();

                    return (T) new Students
                    {
                        ID = rd.GetInt32(0),
                        FirstName = rd.GetString(1),
                        LastName = rd.GetString(2),
                        Age = rd.GetInt32(3),
                        Grade = rd.GetDecimal(4),
                        Email = rd.GetString(5)
                    };
                }

            }
        }

        public void Update(T entity)
        {
            string checkSql = "SELECT COUNT(*) FROM students WHERE StudentID = @ID;";
            string updateSql = @"UPDATE students 
                         SET FirstName = @FirstName, 
                             LastName = @LastName, 
                             Age = @Age, 
                             Grade = @Grade, 
                             Email = @Email 
                         WHERE StudentID = @ID;";

            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (var checkCommand = new SqlCommand(checkSql, conn))
                {
                    Students student = entity as Students;
                    if (student == null)
                    {
                        Console.WriteLine("Invalid student entity.");
                        return;
                    }

                    checkCommand.Parameters.AddWithValue("@ID", student.ID);
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count == 0)
                    {
                        Console.WriteLine("No student found with the given ID.");
                        return;
                    }
                }

                
                using (var command = new SqlCommand(updateSql, conn))
                {
                    Students student = entity as Students;

                    command.Parameters.AddWithValue("@ID", student.ID);
                    command.Parameters.AddWithValue("@FirstName", student.FirstName);
                    command.Parameters.AddWithValue("@LastName", student.LastName);
                    command.Parameters.AddWithValue("@Age", student.Age);
                    command.Parameters.AddWithValue("@Grade", student.Grade);
                    command.Parameters.AddWithValue("@Email", student.Email);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Student updated successfully!");
                    }
                }
            }
        }



    }

}
