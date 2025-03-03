using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem_ADO.NET.Repositories
{
    public interface IRepository<T>
    {
        void Add(T entity);

        void DeleteById(int ID);

        void Update(T entity);

        IEnumerable<T> GetAll();

        T GetByID(int Id);
        
    }
}
