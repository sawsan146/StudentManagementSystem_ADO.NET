using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace StudentManagementSystem_ADO.NET
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            var configration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();
            var conn = new SqlConnection(configration.GetSection("ConnectionString").Value);



        }
    }
}
