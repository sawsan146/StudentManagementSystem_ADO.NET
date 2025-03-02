using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem_ADO.NET.Entities
{
    public class Students
    {
        public int ID { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int? Age { get; set; }

        public string? Email { get; set; }
        public decimal? Grade { get; set; }
    }
}
