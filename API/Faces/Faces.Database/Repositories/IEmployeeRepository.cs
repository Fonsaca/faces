using Faces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faces.Database.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();

        Employee GetById(int id);

        void Create(Employee employee);

        void Update(Employee employee);

        void Delete(int id);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        public void Create(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public Employee GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
